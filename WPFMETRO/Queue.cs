using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPFMETRO
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
        public ObservableCollection<Video> Videos = new ObservableCollection<Video>();

        YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "" });
        private Process ytbDL = new Process();

        private ProcessStartInfo ytbDLInfo = new ProcessStartInfo
        {

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "youtube-dl.exe"
        };

        public Dictionary<string, string> GetFormats(string url)
        {
            Dictionary<string, string> formats = new Dictionary<string, string>();

            if (!url.Contains("youtube.com/playlist?list"))
            {
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
                                    if (values[2].ToString() == "audio")
                                    {
                                        code = values[0].ToString();
                                        desc += "audio";
                                        if (values.Count > 5)
                                        {
                                            desc += values[5].ToString() != "audio" ? " " + values[5].ToString() + ")" : ")";
                                        }
                                        else
                                        {
                                            desc += ")";
                                        }
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
            
            if (formats.Count > 0)
            {
                formats.Add("default", "default");
                formats.Add("mp3", "mp3");
                formats.Add("flac", "flac");
            }
            } else
            {
                formats.Add("default", "default");
                formats.Add("mp3", "mp3");
                formats.Add("flac", "flac");
            }

            return formats;
        }


        public void DownloadPlaylist(string ID, string filename, string path, string filetype, Dictionary<string, string> formats)
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
                    ModifyQueue("https://www.youtube.com/watch?v=" + playlistItem.Snippet.ResourceId.VideoId, filename, path, filetype, formats);
                }

                nextPageToken = playlistItemsListResponse.NextPageToken;
            }
        }

        public void DownloadChannel(string ID, string filename, string path, string filetype, Dictionary<string, string> formats)
        {
            var channelItemsRequest = yt.Channels.List("contentDetails");
            channelItemsRequest.Id = ID.Substring(ID.IndexOf("channel/") + 8);
            channelItemsRequest.MaxResults = 50;

            var channelsListResponse = channelItemsRequest.Execute();

            foreach (var item in channelsListResponse.Items)
            {
                string uploadsListID = item.ContentDetails.RelatedPlaylists.Uploads;

                DownloadPlaylist(uploadsListID, filename, path, filetype, formats);
            }


        }

        public void ModifyQueue(string ID, string filename, string path, string filetype, Dictionary<string, string> formats)
        {
            if (ID.Contains("youtu.be/") || ID.Contains("youtube.com/"))
            {
                if (ID.Contains("playlist"))
                {
                    DownloadPlaylist(ID, filename, path, filetype, formats);
                }
                else if (ID.Contains("channel"))
                {
                    DownloadChannel(ID, filename, path, filetype, formats);
                }
                else
                {
                    var videoRequest = yt.Videos.List("snippet,status");
                    string procID = ID.Contains("youtu.be/") ? ID.Substring(ID.LastIndexOf("/") + 1) : ID.Substring(ID.IndexOf("=") + 1);
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
                        throw new QueueException(Localization.Strings.InvalidURL);
                    }
                    else
                    {
                        foreach (var videoItem in videoListResponse.Items)
                        {
                            if (videoItem.Status != null)
                            {
                                Video video = new Video();
                                video.ID = ID;
                                video.Name = filename;
                                video.Path = path;
                                video.SelectedFormat = filetype;
                                video.ThumbURL = videoItem.Snippet.Thumbnails.Default__.Url;
                                video.Title = videoItem.Snippet.Title;
                                video.AvailableFormats = formats.ToList();

                                Videos.Add(video);
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
                        throw new QueueException(Localization.Strings.InvalidURL);
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

                        Video videoNonYoutube = new Video();
                        videoNonYoutube.ID = ID;
                        videoNonYoutube.Name = filename;
                        videoNonYoutube.Path = path;
                        videoNonYoutube.SelectedFormat = filetype;
                        videoNonYoutube.ThumbURL = null;
                        videoNonYoutube.Title = Regex.Match(HTML, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                        videoNonYoutube.AvailableFormats = formats.ToList();

                        Videos.Add(videoNonYoutube);
                        break;
                }
            }

        }
    }
}
