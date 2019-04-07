using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Google.Apis.Services;
using System.IO;
using Google.Apis.YouTube.v3;
using System.Text.RegularExpressions;
using System.Net;
using youtube_dl.Properties;
using System.Threading;
using System.ComponentModel;
using System.Globalization;
using System.Security.Principal;
using System.Drawing;

namespace youtube_dl
{
    public partial class Form1 : Form
    {
        private const string apiKey = "key"; // Temporary, will fix once safer solution becomes available
        List<Video> downloadQueue = new List<Video>();
        YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = apiKey });
        private int currentVideo = 0;

        private int downloadButtonState = 0;

        static WaitHandle[] waitHandles = new WaitHandle[]
        {
            new AutoResetEvent(false)
        };

        public bool easterEggFound = false;

        public string VerboseStatus
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }

        public string SimplifiedStatus
        {
            get { return DownloadStatus.Text; }
            set { DownloadStatus.Text = value; }
        }

        public int DownloadProgress
        {
            get { return progressBar1.Value; }
            set { progressBar1.Value = value; }
        }

        public string DownloadSpeed
        {
            get { return downloadSpeedLabel.Text; }
            set { downloadSpeedLabel.Text = value; }
        }

        public bool DisplayVerbose
        {
            get { return displayVerboseStatusToolStripMenuItem.Checked; }
        }

        public DataGridView DownloadGridView
        {
            get { return DownloadGrid; }
            set { DownloadGrid = DownloadGridView; }
        }

        public string DownloadButtonText
        {
            get { return DownloadButton.Text; }
            set { DownloadButton.Text = value; }
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.Language != "")
            {
                SetCulture(Settings.Default.Language);
            }

            displayVerboseStatusToolStripMenuItem.Checked = Settings.Default.DisplayStatus;
            FiletypeBox.SelectedIndex = Settings.Default.IndexFileType;
            destinationBox.Text = Settings.Default.Destination == "" ? Application.StartupPath : Settings.Default.Destination;

            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;

            switch (currentCulture.Name)
            {
                case "pt-BR":
                    portuguêsBrasileiroToolStripMenuItem.Checked = true;
                    break;
                case "en-US":
                    englishToolStripMenuItem.Checked = true;
                    break;
            }

            CheckForUpdates();
        }

        private void SetCulture(string culture)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            this.Controls.Clear();
            this.InitializeComponent();
        }

        private bool IsRunningAsAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void CheckForUpdates()
        {
            string guiVersion = Application.ProductVersion;
            string lastVersionYoutubeDL = "";
            string lastVersionGUI = "";

            try
            {
                using (WebClient client = new WebClient())
                {
                    lastVersionGUI = client.DownloadString("https://diskpro.github.io/Youtube-DL-GUI/update/LATEST_VERSION");

                    if (guiVersion != lastVersionGUI.Substring(0, lastVersionGUI.IndexOf('\n')))
                    {
                        DialogResult userDialogResult = MessageBox.Show(strings.AskToUpdateGUI + "\n" + strings.CurrentVersion + guiVersion + "\n" + strings.NewVersion + lastVersionGUI, "", MessageBoxButtons.YesNo);
                        if (userDialogResult == DialogResult.Yes)
                        {
                            Process.Start("https://diskpro.github.io/Youtube-DL-GUI/");
                        }
                    }
                    else
                    {
                        lastVersionYoutubeDL = client.DownloadString("https://rg3.github.io/youtube-dl/update/LATEST_VERSION");

                        if (lastVersionYoutubeDL != Settings.Default.CurrentVersionYoutubeDL)
                        {
                            if (!IsRunningAsAdmin())
                            {
                                ProcessStartInfo ytDLGUI = new ProcessStartInfo();
                                ytDLGUI.UseShellExecute = true;
                                ytDLGUI.WorkingDirectory = Environment.CurrentDirectory;
                                ytDLGUI.FileName = Application.ExecutablePath;
                                ytDLGUI.Verb = "runas";

                                DialogResult userDialogResult = MessageBox.Show(strings.AskToUpdateYoutubeDL + "\n" + strings.CurrentVersion + Settings.Default.CurrentVersionYoutubeDL + "\n" + strings.NewVersion + lastVersionYoutubeDL, "", MessageBoxButtons.YesNo);

                                if (userDialogResult == DialogResult.Yes)
                                {
                                    try
                                    {
                                        Process.Start(ytDLGUI);
                                    }
                                    catch
                                    {
                                        MessageBox.Show(strings.UnauthorizedAccess, strings.Error);
                                    }

                                    Application.Exit();
                                }
                            }
                            else
                            {
                                if (lastVersionYoutubeDL != Settings.Default.CurrentVersionYoutubeDL)
                                {
                                    DownloadStatus.Text = strings.Updating;

                                    if (File.Exists(Application.StartupPath + @"\youtube-dl.exe"))
                                    {
                                        File.Delete(Application.StartupPath + @"\youtube-dl.exe");
                                    }
                                    client.DownloadFile("https://yt-dl.org/downloads/" + lastVersionYoutubeDL + @"\youtube-dl.exe", Application.StartupPath + @"\youtube-dl.exe");

                                    Settings.Default.CurrentVersionYoutubeDL = lastVersionYoutubeDL;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException || ex is WebException)
                {
                    MessageBox.Show(strings.UnauthorizedAccess, strings.Error);
                }
            }

        DownloadStatus.Text = strings.NoDownload;      
        }

        private void DownloadPlaylist(string ID, string filename, string path, int filetype)
        {
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = ID.Substring(ID.IndexOf("=") + 1);
                playlistItemsListRequest.MaxResults = 50;
                playlistItemsListRequest.PageToken = nextPageToken;

                var playlistItemsListResponse = playlistItemsListRequest.Execute();

                foreach (var playlistItem in playlistItemsListResponse.Items)
                {
                    Video videoFromPlaylist = new Video(this);
                    videoFromPlaylist.title = playlistItem.Snippet.Title;
                    videoFromPlaylist.ID = "https://www.youtube.com/watch?v=" + playlistItem.Snippet.ResourceId.VideoId;
                    videoFromPlaylist.name = filename;
                    videoFromPlaylist.path = path;
                    videoFromPlaylist.filetype = filetype;
                    videoFromPlaylist.thumbURL = playlistItem.Snippet.Thumbnails.Default__.Url;

                    BeginInvoke((Action)(() =>
                    {
                        DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, playlistItem.Snippet.Title, videoFromPlaylist.ID);
                    }));

                    downloadQueue.Add(videoFromPlaylist);
                }

                nextPageToken = playlistItemsListResponse.NextPageToken;
            }
        }

        private void DownloadChannel(string ID, string filename, string path, int filetype)
        {
            var channelItemsRequest = yt.Channels.List("contentDetails");
            channelItemsRequest.Id = ID.Substring(ID.IndexOf("channel/") + 8);
            channelItemsRequest.MaxResults = 50;

            var channelsListResponse = channelItemsRequest.Execute();

            foreach(var item in channelsListResponse.Items)
            {
                string uploadsListID = item.ContentDetails.RelatedPlaylists.Uploads;

                Thread downloadPlaylistTask = new Thread(() => DownloadPlaylist(uploadsListID, filename, path, filetype));
                downloadPlaylistTask.Start();
            }


        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            switch(downloadButtonState) 
            {
                case 0:
                downloadVideoWorker.RunWorkerAsync();
                    DownloadButton.Enabled = false;
                    queueButton.Enabled = false;
                    AddFromTextButton.Enabled = false;
                    break;

                case 1:
                    string newFilename = UseTitleInEditCheckbox.Checked ? "%(title)s.%(ext)s" :  EditFilenameBox.Text;
                    string newPath = Directory.Exists(folderBrowserDialog.SelectedPath) ? folderBrowserDialog.SelectedPath + @"\": downloadQueue[DownloadGrid.SelectedRows[0].Index].path;
                    ModifyQueue(EditIDBox.Text, newFilename, newPath, EditFiletypeBox.SelectedIndex, true);

                    DownloadButton.Text = strings.Download;
                    break;
            }
        }

        private void UseTitleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            filenameBox.ReadOnly = useTitleCheckbox.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Destination = destinationBox.Text;
            Settings.Default.IndexFileType = FiletypeBox.SelectedIndex;
            Settings.Default.DisplayStatus = displayVerboseStatusToolStripMenuItem.Checked;

            Settings.Default.Save(); // Saves user config
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            if (Directory.Exists(folderBrowserDialog.SelectedPath))
            {
                destinationBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void ModifyQueue(string ID, string filename, string path, int filetype, bool isEdit)
        {
            switch (ID.Length)
            {
                case 43:
                    var videoRequest = yt.Videos.List("snippet");
                    videoRequest.Id = ID.Contains("youtube") ? ID.Substring(ID.IndexOf("=") + 1) : ID;

                    var videoListResponse = videoRequest.Execute();

                    if (videoListResponse.Items.Count < 1)
                    {
                        MessageBox.Show(strings.InvalidVideo, strings.Error);
                        break;
                    }

                    foreach (var videoItem in videoListResponse.Items)
                    {
                        Video video = new Video(this);
                        video.ID = ID;
                        video.name = filename;
                        video.path = path;
                        video.filetype = filetype;
                        video.thumbURL = videoItem.Snippet.Thumbnails.Default__.Url;
                        video.title = videoItem.Snippet.Title;

                        if (isEdit)
                        {
                            downloadQueue[DownloadGrid.SelectedRows[0].Index] = video;
                            DownloadGrid[1, DownloadGrid.SelectedRows[0].Index].Value = video.title;
                            DownloadGrid[2, DownloadGrid.SelectedRows[0].Index].Value = video.ID;

                            UpdateCard();
                        }
                        else
                        {
                            downloadQueue.Add(video);
                            DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, video.title, ID);
                        }

                    }
                    break;
                case 0:
                    MessageBox.Show(strings.InvalidURL, strings.Error);
                    break;
                default:
                    if (ID.Contains("youtube"))
                    {
                        if (ID.Contains("playlist"))
                        {
                            Thread downloadPlaylistTask = new Thread(() => DownloadPlaylist(ID, filename, path, filetype));
                            downloadPlaylistTask.Start();
                        }
                        else if (ID.Contains("channel"))
                        {
                            DownloadChannel(ID, filename, path, filetype);
                        }
                    } else
                    {
                        string HTML = "";

                        using (WebClient client = new WebClient())
                        {
                            try
                            {
                                HTML = client.DownloadString(UrlBox.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(strings.UnableGatherTitle, strings.Error);
                            }
                        }

                        Video videoNonYoutube = new Video(this);
                        videoNonYoutube.ID = ID;
                        videoNonYoutube.name = filename;
                        videoNonYoutube.path = path;
                        videoNonYoutube.filetype = filetype;
                        videoNonYoutube.thumbURL = null;
                        videoNonYoutube.title = Regex.Match(HTML, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

                        if (isEdit)
                        {
                            downloadQueue[DownloadGrid.SelectedRows[0].Index] = videoNonYoutube;
                            DownloadGrid[1, DownloadGrid.SelectedRows[0].Index].Value = videoNonYoutube.title;
                            DownloadGrid[2, DownloadGrid.SelectedRows[0].Index].Value = videoNonYoutube.ID;

                            UpdateCard();
                        }
                        else
                        {
                            downloadQueue.Add(videoNonYoutube);
                            DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, videoNonYoutube.title, ID);
                        }
                    }
                    break;
            }
            UrlBox.Clear();
        }

        // Adds selected video to download queue
        private void QueueButton_Click(object sender, EventArgs e)
        {
            string ID = UrlBox.Text;
            string filename = useTitleCheckbox.Checked ? "%(title)s.%(ext)s" : filenameBox.Text;
            string path = destinationBox.Text + @"\";
            int filetype = FiletypeBox.SelectedIndex;

            ModifyQueue(ID, filename, path, filetype, false);
        }

        public void ClearCard()
        {
            ThumbnailBox.Image = null;
            TitleCard.Text = "";
            IDCard.Text = "";
            FiletypeCard.Text = "";
            PathCard.Text = "";
            FilenameCard.Text = "";
        }

        private void DownloadVideoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var video in downloadQueue)
            {
                video.DownloadVideo();
                currentVideo++;
            }
            downloadQueue.Clear();
            currentVideo = 0;

            BeginInvoke((Action)(() =>
            {
                DownloadButton.Enabled = true;
                AddFromTextButton.Enabled = true;
                queueButton.Enabled = true;
                
                DownloadButton.Text = strings.Download;
                if(alertOnFinishToolStripMenuItem.Checked)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    MessageBox.Show(strings.Finished);
                }
            }));
        }

        private void DeleteVideo(DataGridViewRow row)
        {
            if (DownloadGrid.SelectedRows.Count > 0)
            {
                row = DownloadGrid.SelectedRows[0];

                downloadQueue.RemoveAt(row.Index);
                DownloadGrid.Rows.RemoveAt(row.Index);

                ClearCard();
            }
        }

        private void DownloadGrid_SelectionChanged(object sender, EventArgs e)
        {
            UpdateCard();
            if(DownloadGrid.Rows.Count > 0)
            {
                ExportButton.Visible = true;
                DownloadButton.Enabled = true;
            }
            else
            {
                ExportButton.Visible = false;
                DownloadButton.Enabled = false;
            }
        }

        private void UpdateCard()
        {
            if (DownloadGrid.RowCount > 0)
            {
                if (DownloadGrid.SelectedRows.Count > 0)
                {
                    IDCard.Visible = true;
                    FiletypeCard.Visible = true;
                    PathCard.Visible = true;
                    FilenameCard.Visible = true;
                    AddFromTextButton.Enabled = true;
                    queueButton.Enabled = true;

                    EditDestinationButton.Visible = false;
                    EditFiletypeBox.Visible = false;
                    EditIDBox.Visible = false;
                    EditFilenameBox.Visible = false;
                    UseTitleInEditCheckbox.Visible = false;
                    downloadButtonState = 0;

                    int index = int.Parse(DownloadGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1;
                    Video video = downloadQueue[index];
                    string filename = video.name == "%(title)s.%(ext)s" ? video.title : video.name;

                    ThumbnailBox.Image = video.GetThumbnail();
                    TitleCard.Text = video.title.Length > 60 ? video.title.Substring(0, 57) + "..." : video.title;
                    IDCard.Text = video.ID;
                    FiletypeCard.Text = FiletypeBox.Items[video.filetype].ToString();
                    PathCard.Text = video.path;
                    FilenameCard.Text = filename.Length > 60 ? filename.Substring(0, 57) + "..." : filename;
                }
            }
        }

        private void PortuguêsBrasileiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Language = "pt-BR";
            MessageBox.Show("Para efetivar esta mudança, feche e abra o programa de novo");
        }

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Language = "en-US";
            MessageBox.Show("In order to apply these changes, you'll need to restart the program");
        }

        private void AddFromTextButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string[] videos = File.ReadAllLines(openFileDialog1.FileName);
            string filename = useTitleCheckbox.Checked ? "%(title)s.%(ext)s" : filenameBox.Text;
            string path = destinationBox.Text + "/";
            int filetype = FiletypeBox.SelectedIndex;

            foreach (string video in videos)
            {
                ModifyQueue(video, filename, path, filetype, false);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        private void DownloadGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DeleteVideo(e.Row);
        }

        private void DownloadGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = DownloadGrid.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    DataGridViewRow row = DownloadGridView.Rows[hit.RowIndex];

                    DownloadGrid.ClearSelection();
                    row.Selected = true;

                    contextMenuStrip.Show(DownloadGrid, e.X, e.Y);
                }
            }
        }

        private void deleteContextMenuItem_Click(object sender, EventArgs e)
        {
            if (DownloadGrid.SelectedRows.Count > 0)
            {
                DeleteVideo(DownloadGrid.SelectedRows[0]);
            }
        }

        private void editStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DownloadGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = DownloadGridView.SelectedRows[0];

                Video editedVideo = downloadQueue[row.Index];

                IDCard.Visible = false;
                FiletypeCard.Visible = false;
                PathCard.Visible = false;
                AddFromTextButton.Enabled = false;
                FilenameCard.Visible = false;
                queueButton.Enabled = false;

                EditDestinationButton.Visible = true;
                EditFiletypeBox.Visible = true;
                EditIDBox.Visible = true;
                EditFilenameBox.Visible = true;
                UseTitleInEditCheckbox.Visible = true;
                DownloadButton.Text = strings.SaveChanges;
                downloadButtonState = 1;

                EditFiletypeBox.SelectedIndex = editedVideo.filetype;
                EditIDBox.Text = editedVideo.ID;
                if (editedVideo.name == "%(title)s.%(ext)s")
                {
                    EditFilenameBox.Text = editedVideo.title;
                    EditFilenameBox.Enabled = false;
                }
                else
                {
                    EditFilenameBox.Text = editedVideo.name;
                }
                UseTitleInEditCheckbox.Checked = editedVideo.name == "%(title)s.%(ext)s";
            }
        }

        private void EditDestinationButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
        }

        private void UseTitleInEditCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            DataGridViewRow row = DownloadGridView.SelectedRows[0];
            Video editedVideo = downloadQueue[row.Index];

            if (UseTitleInEditCheckbox.Checked)
            {
                EditFilenameBox.Text = editedVideo.title;
                EditFilenameBox.Enabled = false;
            }
            else
            {
                EditFilenameBox.Enabled = true;
            }
        }

        private void FiletypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void converterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Converter converter = new Converter();
            converter.Show();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            string queueString = "";
            exportQueueDialog.ShowDialog();

            
            foreach (Video video in downloadQueue)
                {
                    queueString += video.ID + Environment.NewLine;
                }
            File.WriteAllText(exportQueueDialog.FileName, queueString);
        }
    }
}
