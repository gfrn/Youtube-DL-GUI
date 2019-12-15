using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using youtube_dl.Properties;

namespace youtube_dl
{
    public class QueueException : Exception
    {
        public QueueException(string message)
           : base(message)
        {
        }
    }
    class Queue
    {
        private const string apiKey = "key"; // Temporary, will fix once safer solution becomes available
        List<Video> videos = new List<Video>();
        YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = apiKey });
        private Process ytbDL = new Process();

        private ProcessStartInfo ytbDLInfo = new ProcessStartInfo
        {

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "youtube-dl.exe"
        };

        private readonly Form1 f;
        public Queue(Form1 form)
        {
            this.f = form;
        }

        public List<Video> Videos { get => videos; set => videos = value; }

        public Dictionary<string, string> getFormats(string url)
        {
            Dictionary<string, string> formats = new Dictionary<string, string>();

            string output = "";
            string arguments = "-F " + url;
            bool isNext = false;

            ytbDLInfo.Arguments = arguments;

            ytbDL = Process.Start(ytbDLInfo);

            ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                (s, f) =>
                {
                    output = f.Data ?? "null";

                    if (output != "null")
                    {
                        if (isNext)
                        {
                            string code, desc;

                            MatchCollection values = Regex.Matches(output, @"([^\s]+)");
                            if (values.Count > 4)
                            {
                                desc = values[1].ToString() + " (";
                                if(values[2].ToString() == "audio")
                                    {
                                        code = values[0].ToString();
                                        desc += values[2].ToString();
                                        desc += values.Count  > 5 ? " " + values[5].ToString() + ")" : ")";
                                }
                                else
                                    {
                                        code = values[0].ToString() + ' ' + values[1].ToString();
                                        desc += values[2].ToString() + ' ' + values[3].ToString() + ')';
                                    }                  
                            }
                            else
                            {
                                code = values[0].ToString();
                                desc = values[1].ToString();
                            }

                            formats.Add(code, desc);
                        }
                        else if (output.IndexOf(" ") > 0) { isNext = output.Substring(0, output.IndexOf(" ")) == "format"; }
                    }
                });

            ytbDL.Start();
            ytbDL.BeginOutputReadLine();
            ytbDL.WaitForExit();
            ytbDL.CancelOutputRead();

            if(formats.Count > 0)
            {
                formats.Add("default", "default (mp4)");
                formats.Add("mp3", "mp3");
                formats.Add("flac", "flac");
            }

            return formats;
        }

        public void DownloadPlaylist(string ID, string filename, string path, string filetype, string desc)
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
                    ModifyQueue("https://www.youtube.com/watch?v=" + playlistItem.Snippet.ResourceId.VideoId, filename, path, filetype, desc, false);
                }

                nextPageToken = playlistItemsListResponse.NextPageToken;
            }
        }

        public void DownloadChannel(string ID, string filename, string path, string filetype, string desc)
        {
            var channelItemsRequest = yt.Channels.List("contentDetails");
            channelItemsRequest.Id = ID.Substring(ID.IndexOf("channel/") + 8);
            channelItemsRequest.MaxResults = 50;

            var channelsListResponse = channelItemsRequest.Execute();

            foreach (var item in channelsListResponse.Items)
            {
                string uploadsListID = item.ContentDetails.RelatedPlaylists.Uploads;

                DownloadPlaylist(uploadsListID, filename, path, filetype, desc);
            }


        }

        public void ModifyQueue(string ID, string filename, string path, string filetype, string desc, bool isEdit)
        {
            string procID = ID;

            if (ID.Contains("youtu.be/") || ID.Contains("youtube.com/"))
            {
                if (ID.Contains("playlist"))
                {
                    DownloadPlaylist(ID, filename, path, filetype, desc);
                }
                else if (ID.Contains("channel"))
                {
                    DownloadChannel(ID, filename, path, filetype, desc);
                }
                else
                {
                    var videoRequest = yt.Videos.List("snippet,status");
                    procID = ID.Contains("youtu.be/") ? ID.Substring(ID.LastIndexOf("/") + 1) : ID.Substring(ID.IndexOf("=") + 1);

                    if (procID.Contains("&"))
                    {
                        procID = procID.Substring(0, procID.IndexOf("&"));
                    }
                    else if (procID.Contains("?"))
                    {
                        procID = procID.Substring(0, procID.IndexOf("?"));
                    }

                    videoRequest.Id = procID;

                    var videoListResponse = videoRequest.Execute();

                    if (videoListResponse.Items.Count < 1)
                    {
                        throw new QueueException(strings.InvalidURL);
                    }
                    else
                    {
                        foreach (var videoItem in videoListResponse.Items)
                        {
                            if (videoItem.Status != null)
                            {
                                Video video = new Video(f);
                                video.ID = ID;
                                video.name = filename;
                                video.path = path;
                                video.filetype = filetype;
                                video.filetypeDesc = desc;
                                video.thumbURL = videoItem.Snippet.Thumbnails.Default__.Url;
                                video.title = videoItem.Snippet.Title;

                                if (isEdit)
                                {
                                    videos[f.DownloadGridView.SelectedRows[0].Index] = video;
                                    f.DownloadGridView[1, f.DownloadGridView.SelectedRows[0].Index].Value = video.title;
                                    f.DownloadGridView[2, f.DownloadGridView.SelectedRows[0].Index].Value = video.ID;

                                    f.UpdateCard();
                                }
                                else
                                {
                                    videos.Add(video);
                                    f.DownloadGridView.Rows.Add(f.DownloadGridView.Rows.Count + 1, video.title, ID);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                switch (ID.Length)
                {
                    case 0:
                        throw new QueueException(strings.InvalidURL);
                    default:
                        string HTML = "";

                        using (WebClient client = new WebClient())
                        {
                            try
                            {
                                HTML = client.DownloadString(ID);
                            }
                            catch (Exception)
                            {
                                HTML = "<title>?</title>";
                            }
                        }

                        Video videoNonYoutube = new Video(f);
                        videoNonYoutube.ID = ID;
                        videoNonYoutube.name = filename;
                        videoNonYoutube.path = path;
                        videoNonYoutube.filetype = filetype;
                        videoNonYoutube.filetypeDesc = desc;
                        videoNonYoutube.thumbURL = null;
                        videoNonYoutube.title = Regex.Match(HTML, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

                        if (isEdit)
                        {
                            videos[f.DownloadGridView.SelectedRows[0].Index] = videoNonYoutube;
                            f.DownloadGridView[1, f.DownloadGridView.SelectedRows[0].Index].Value = videoNonYoutube.title;
                            f.DownloadGridView[2, f.DownloadGridView.SelectedRows[0].Index].Value = videoNonYoutube.ID;

                            f.UpdateCard();
                        }
                        else
                        {
                            videos.Add(videoNonYoutube);
                            f.DownloadGridView.Rows.Add(f.DownloadGridView.Rows.Count + 1, videoNonYoutube.title, ID);
                        }
                        break;
                }
            }

        }
    }
}
