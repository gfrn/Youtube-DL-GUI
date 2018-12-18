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

namespace youtube_dl
{
    public partial class Form1 : Form
    {
        private const string apiKey = "key"; // Temporary, will fix once safer solution becomes available
        List<Video> downloadQueue = new List<Video>();
        YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = apiKey });

        static WaitHandle[] waitHandles = new WaitHandle[]
        {
            new AutoResetEvent(false)
        };

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
            get { return displayDownloadStatusTextToolStripMenuItem.Checked; }
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

            displayDownloadStatusTextToolStripMenuItem.Checked = Settings.Default.DisplayStatus;
            FiletypeBox.SelectedIndex = Settings.Default.IndexFileType;
            destinationBox.Text = Settings.Default.Destination == "" ? Application.StartupPath : Settings.Default.Destination;

            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;

            switch (currentCulture.Name) {
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

        private void CheckForUpdates()
        {
            string HTML = "";
            string lastVersion = "";

            try
            {
                using (WebClient client = new WebClient())
                {
                    HTML = client.DownloadString("https://rg3.github.io/youtube-dl/download.html");

                    lastVersion = HTML.Substring(HTML.IndexOf("https://yt-dl.org/downloads/") + 28, 10);

                    if (lastVersion != Settings.Default.CurrentVersion)
                    {
                        DownloadStatus.Text = strings.Updating;


                        if (File.Exists(Application.StartupPath + @"\youtube-dl.exe"))
                        {
                            File.Delete(Application.StartupPath + @"\youtube-dl.exe");
                        }
                        client.DownloadFile("https://yt-dl.org/downloads/" + lastVersion + @"\youtube-dl.exe", Application.StartupPath);

                        Settings.Default.CurrentVersion = lastVersion;
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
                    videoFromPlaylist.ID = "https://www.youtube.com/watch?v=" + playlistItem.Snippet.ResourceId.VideoId;
                    videoFromPlaylist.name = filename;
                    videoFromPlaylist.path = path;
                    videoFromPlaylist.filetype = filetype;

                    BeginInvoke((Action)(() =>
                    {
                        DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, playlistItem.Snippet.Title, videoFromPlaylist.ID);
                    }));

                    downloadQueue.Add(videoFromPlaylist);
                }

                nextPageToken = playlistItemsListResponse.NextPageToken;
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (DownloadButton.Text == strings.Cancel)
            {
                Video video = new Video(this);
                video.AbortDownloads();
            }
            else
            {
                downloadVideoWorker.RunWorkerAsync();
                DownloadButton.Text = strings.Cancel;
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
            Settings.Default.DisplayStatus = displayDownloadStatusTextToolStripMenuItem.Checked;

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

        private void AddVideo(string ID, string filename, string path, int filetype)
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

                        downloadQueue.Add(video);

                        DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, video.title, ID);
                    }

                    DownloadButton.Enabled = true;
                    deleteButton.Enabled = true;
                    break;
                case 72:
                    Thread downloadPlaylistTask = new Thread(() => DownloadPlaylist(ID, filename, path, filetype));
                    downloadPlaylistTask.Start();

                    DownloadButton.Enabled = true;
                    deleteButton.Enabled = true;
                    break;
                case 0:
                    MessageBox.Show(strings.InvalidURL, strings.Error);
                    break;
                default:
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

                    downloadQueue.Add(videoNonYoutube);

                    DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, videoNonYoutube.title, ID);

                    DownloadButton.Enabled = true;
                    deleteButton.Enabled = true;
                    break;
            }

            UrlBox.Clear();
        }

        // Adds selected video to download queue
        private void QueueButton_Click(object sender, EventArgs e)
        {
            string ID = UrlBox.Text;
            string filename = useTitleCheckbox.Checked ? "/%(title)s.%(ext)s" : "/" + filenameBox.Text;
            string path = destinationBox.Text;
            int filetype = FiletypeBox.SelectedIndex;

            AddVideo(ID, filename, path, filetype);
        }

        public void ClearCard()
        {
            ThumbnailBox.Image = null;
            TitleCard.Text = "";
            IDCard.Text = "";
            FiletypeCard.Text = "";
            PathCard.Text = "";
        }

        private void DownloadVideoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                queueButton.Enabled = false;
                deleteButton.Enabled = false;
            }));
            
            foreach (var video in downloadQueue)
            {
                video.DownloadVideo();
            }
            downloadQueue.Clear();

            BeginInvoke((Action)(() =>
            {
                queueButton.Enabled = true;
                
                DownloadButton.Text = strings.Download;
            }));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = DownloadGrid.SelectedRows[0];

            downloadQueue.RemoveAt(row.Index);
            DownloadGrid.Rows.RemoveAt(row.Index);

            if(downloadQueue.Count < 1)
            {
                deleteButton.Enabled = false;
            }

            ClearCard();
        }

        private void DownloadGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DownloadGrid.RowCount > 0)
            {
                DataGridViewRow row = DownloadGrid.SelectedRows[0];
                if (row.Index >= 0)
                {
                    Video video = downloadQueue[row.Index];

                    ThumbnailBox.Image = video.GetThumbnail();
                    TitleCard.Text = video.title.Length > 60 ? video.title.Substring(0,57) + "..." : video.title;
                    IDCard.Text = video.ID;
                    FiletypeCard.Text = FiletypeBox.Items[video.filetype].ToString();
                    PathCard.Text = video.path;
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

        private void DisplayDownloadStatusTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayDownloadStatusTextToolStripMenuItem.Checked = !displayDownloadStatusTextToolStripMenuItem.Checked;
        }

        private void AddFromTextButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string[] videos = File.ReadAllLines(openFileDialog1.FileName);
            string filename = useTitleCheckbox.Checked ? "/%(title)s.%(ext)s" : "/" + filenameBox.Text;
            string path = destinationBox.Text;
            int filetype = FiletypeBox.SelectedIndex;

            foreach (string video in videos)
            {
                AddVideo(video, filename, path, filetype);
            }
        }
    }
}
