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

namespace youtube_dl
{
    public partial class Form1 : Form
    {
        public const string apiKey = "key"; // Temporary, will fix once safer solution becomes available
        List<Video> downloadQueue = new List<Video>();

        static WaitHandle[] waitHandles = new WaitHandle[]
        {
            new AutoResetEvent(false)
        };

        YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = apiKey });

        // Stores filetype IDs for Youtube-DL.
        public Dictionary<int, string> fileTypes = new Dictionary<int, string>()
        {
            {0, "140"},
            {1, "160"},
            {2, "133"},
            {3, "134"},
            {4, "135"},
            {5, "136"},
            {6, "17"},
            {7, "36"},
            {8, "5"},
            {9, "43"},
            {10, "18"},
            {11, "22"}
        };

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            FiletypeBox.SelectedIndex = Settings.Default.IndexFileType;
            destinationBox.Text = Settings.Default.Destination == "" ? Application.StartupPath : Settings.Default.Destination;
        }
        
        Process ytbDL = new Process
        {
            StartInfo =
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "cmd.exe"
        }

        };


        private void downloadPlaylist(string ID, string filename, string path, int filetype)
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
                    Video videoFromPlaylist = new Video();
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
            downloadVideoWorker.RunWorkerAsync();

            DownloadButton.Enabled = false;
        }

        private void useTitleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            filenameBox.ReadOnly = useTitleCheckbox.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Destination = destinationBox.Text;
            Settings.Default.IndexFileType = FiletypeBox.SelectedIndex;

            Settings.Default.Save(); // Saves user config
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            if (Directory.Exists(folderBrowserDialog.SelectedPath))
            {
                destinationBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        // Adds selected video to download queue
        private void queueButton_Click(object sender, EventArgs e)
        {
            string ID = UrlBox.Text;
            string filename = useTitleCheckbox.Checked ? "/%(title)s.%(ext)s" : "/" + filenameBox.Text;
            string path = destinationBox.Text;
            int filetype = FiletypeBox.SelectedIndex;

            switch (ID.Length)
            {
                case 43:
                    var videoRequest = yt.Videos.List("snippet");
                    videoRequest.Id = ID.Contains("youtube") ? ID.Substring(ID.IndexOf("=") + 1) : ID;

                    var videoListResponse = videoRequest.Execute();

                    if (videoListResponse.Items.Count < 1)
                    {
                        MessageBox.Show("Invalid Video!");
                        break;
                    }

                    foreach (var videoItem in videoListResponse.Items)
                    {
                        DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, videoItem.Snippet.Title, ID);
                    }

                    Video video = new Video();
                    video.ID = ID;
                    video.name = filename;
                    video.path = path;
                    video.filetype = filetype;

                    downloadQueue.Add(video);

                    DownloadButton.Enabled = true;

                    break;
                case 72:
                    Thread downloadPlaylistTask = new Thread(() => downloadPlaylist(ID, filename, path, filetype));
                    downloadPlaylistTask.Start();

                    DownloadButton.Enabled = true;

                    break;
                default:
                    WebClient client = new WebClient();
                    string HTML = client.DownloadString(UrlBox.Text);

                    string title = Regex.Match(HTML, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

                    DownloadGrid.Rows.Add(DownloadGrid.Rows.Count + 1, title, ID);

                    Video videoNonYoutube = new Video();
                    videoNonYoutube.ID = ID;
                    videoNonYoutube.name = filename;
                    videoNonYoutube.path = path;
                    videoNonYoutube.filetype = filetype;

                    downloadQueue.Add(videoNonYoutube);

                    DownloadButton.Enabled = true;

                    break;
            }

            UrlBox.Clear();
        }

        private void downloadVideoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                queueButton.Enabled = false;
                deleteButton.Enabled = false;
            }));

            string arguments = "/c youtube-dl ";
            foreach (var video in downloadQueue)
            {
                string output = "";
                bool completedDownload = false;
                string progress = "";

                if (!Directory.Exists(video.path))
                {
                    MessageBox.Show("Invalid path selected! Check if folder exists and try again!","Error!");
                }
                else
                {
                    arguments += UseDefaultFormatBox.Checked ? "" : "-f " + fileTypes[video.filetype];
                    arguments += " -o \"" + video.path + video.name + "\"";
                    arguments += " " + video.ID;

                    ytbDL.StartInfo.Arguments = arguments;
                    ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                    (s, f) =>
                    {
                        DownloadStatus.Text = "Starting Up...";
                        output = f.Data ?? "null";

                        if (output.Contains("[download]") && output.Contains("of"))
                        {
                            if (output.Contains("at") && output.Contains("MiB") && !output.Contains("Destination"))
                            {
                                string downloadSpeed = output.Substring(output.IndexOf("at") + 3, output.IndexOf("ETA") - output.IndexOf("at") - 3);
                                downloadSpeedLabel.Text = downloadSpeed;
                                progress = output.Substring(output.LastIndexOf("[download]") + 11, output.LastIndexOf("%") - 11);
                                progress = progress.Contains(".") ? progress.Substring(0, progress.IndexOf(".")) : progress;
                            }
                            DownloadStatus.Text = "Downloading...";

                            BeginInvoke((Action)(() =>
                            {
                                progressBar1.Value = progress != "" ? Int16.Parse(progress) : 0;
                            }));
                            completedDownload = true;
                        }

                        BeginInvoke((Action)(() =>
                        {
                            if(output != "null")
                            {
                                statusLabel.Text = output;
                            }
                        }));
                    }
                    );

                    ytbDL.Start();
                    ytbDL.BeginOutputReadLine();
                    ytbDL.WaitForExit();
                    ytbDL.CancelOutputRead();

                    if(!completedDownload)
                    {
                        MessageBox.Show("An error has occured! Try using other file type or the default one this time.", "Error!");
                        statusLabel.Text = "";
                    }

                    downloadSpeedLabel.Text = "0.0 MiB/s";
                    BeginInvoke((Action)(() =>
                    {
                        progressBar1.Value = 0;
                        DownloadGrid.Rows.RemoveAt(0);
                    }));

                    DownloadStatus.Text = "No Download";

                    arguments = "/c youtube-dl ";
                }
            }
            downloadQueue.Clear();

            BeginInvoke((Action)(() =>
            {
                queueButton.Enabled = true;
                deleteButton.Enabled = true;
            }));
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DownloadGrid.SelectedRows)
            {
                downloadQueue.RemoveAt(row.Index);
                DownloadGrid.Rows.RemoveAt(row.Index);
            }
        }

        private void UseDefaultFormatBox_CheckedChanged(object sender, EventArgs e)
        {
            FiletypeBox.Enabled = !UseDefaultFormatBox.Checked;
        }
    }

    public class Video
    {
        public string ID { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public int filetype { get; set; }
    }
}
