using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class VideoJSON
{
    public string id { get; set; }
    public string title { get; set; }
}
class Playlist
{
    public List<VideoJSON> entries { get; set; }
}

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

        private Process ytbDL = new Process();

        private ProcessStartInfo ytbDLInfo = new ProcessStartInfo
        {

            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.GetEncoding(65001),
            FileName = "youtube-dl.exe"
        };

        public List<string> GetInfo(string url)
        {
            string[] args = { "--encoding utf-8 --skip-download --get-title --no-warnings " + url, "--encoding utf-8 --skip-download --list-thumbnails --no-warnings " + url };
            List<string> response = new List<string>();
            bool isThumb = false;
            bool isNext = false;

            foreach (string argument in args)
            {
                ytbDLInfo.Arguments = argument;
                ytbDL = Process.Start(ytbDLInfo);
                string info = null;

                ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                    (s, f) =>
                    {
                        string output = f.Data ?? "null";
                        if (isThumb)
                        {
                            if (isNext && info == null)
                            {
                                info = output == "null" || output.Contains("No thumbnail") ? info : output.Substring(output.LastIndexOf(' ') + 1);
                            }
                            else
                            {
                                isNext = output == null ? false : output.Contains("width");
                            }
                        }
                        else
                        {
                            info = output == "null" ? info : output;
                        }
                    });

                ytbDL.Start();
                ytbDL.BeginOutputReadLine();
                ytbDL.WaitForExit();
                ytbDL.CancelOutputRead();

                response.Add(info);
                isThumb = true;
            }

            return response;
        }

        public Dictionary<string, string> GetFormats(string url)
        {
            Dictionary<string, string> formats = new Dictionary<string, string>();

            if (!url.Contains("youtube.com/playlist?list"))
            {
                string output = "";
                string arguments = "-F " + url;
                bool isNext = false;
                bool hasAudio = false;

                ytbDLInfo.Arguments = arguments;

                ytbDL = Process.Start(ytbDLInfo);

                ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                    (s, f) =>
                    {
                        output = f.Data ?? "null";

                        if (output != "null" && !output.Contains("["))
                        {
                            if (isNext)
                            {
                                string code, desc;

                                MatchCollection values = Regex.Matches(output, @"([^\s]+)");
                                code = values[0].ToString();
                                if (values.Count > 4)
                                {
                                    if (values[2].ToString() == "audio")
                                    {
                                        hasAudio = true;
                                    }
                                    else
                                    {
                                        code += ' ' + values[1].ToString();
                                    }
                                }

                                code += output.Contains("video only") ? "-y" : "-n";
                                desc = output.Substring(output.IndexOf(" ")).Replace("only", "").TrimStart();

                                formats.Add(code, desc);
                            }
                            else if (output.IndexOf(" ") > 0) { isNext = output.Substring(0, output.IndexOf(" ")) == "format"; }
                        }
                    });

                ytbDL.Start();
                ytbDL.BeginOutputReadLine();
                ytbDL.WaitForExit();
                ytbDL.CancelOutputRead();

                if (formats.Count > 1)
                {
                    formats.Add("default", "default");
                    if (formats.Count > 1 && hasAudio)
                    {
                        formats.Add("mp3", "mp3");
                        formats.Add("flac", "flac");
                    }
                }

            }

            return formats;
        }


        public void DownloadPlaylist(string ID, string filename, string path, string filetype, Dictionary<string, string> formats)
        {
            ytbDLInfo.Arguments = String.Format("-J --flat-playlist \"{0}\"", ID);
            ytbDL = Process.Start(ytbDLInfo);
            Playlist playlist = null;

            ytbDL.OutputDataReceived += new DataReceivedEventHandler(
            (s, f) =>
            {
                if (f.Data != null)
                {
                    playlist = JsonSerializer.Deserialize<Playlist>(f.Data);
                }
            });

            ytbDL.Start();
            ytbDL.BeginOutputReadLine();
            ytbDL.WaitForExit();
            ytbDL.CancelOutputRead();

            foreach (VideoJSON v in playlist.entries)
            {
                string url = "https://www.youtube.com/watch?v=" + v.id;
                ModifyQueue(v.title, null, url, filename, path, filetype, formats);
            }
        }

        public void DownloadChannel(string ID, string filename, string path, string filetype, Dictionary<string, string> formats)
        {
            ytbDLInfo.Arguments = String.Format("-J --flat-playlist \"{0}\"/videos", ID);
            ytbDL = Process.Start(ytbDLInfo);
            Playlist playlist = null;

            ytbDL.OutputDataReceived += new DataReceivedEventHandler(
            (s, f) =>
            {
                if (f.Data != null)
                {
                    playlist = JsonSerializer.Deserialize<Playlist>(f.Data);
                }
            });

            ytbDL.Start();
            ytbDL.BeginOutputReadLine();
            ytbDL.WaitForExit();
            ytbDL.CancelOutputRead();

            foreach (VideoJSON v in playlist.entries)
            {
                string url = "https://www.youtube.com/watch?v=" + v.id;
                ModifyQueue(v.title, null, url, filename, path, filetype, formats);
            }
        }

        public void ModifyQueue(string title, string thumbURL, string ID, string filename, string path, string filetype, Dictionary<string, string> formats)
        {
            if (ID.Contains("youtu.be/") || ID.Contains("youtube.com/"))
            {
                if (ID.Contains("list="))
                {
                    DownloadPlaylist(ID, filename, path, filetype, formats);
                }
                else if (ID.Contains("channel"))
                {
                    DownloadChannel(ID, filename, path, filetype, formats);
                }
                else
                {
                    Video video = new Video
                    {
                        ID = ID,
                        Name = filename,
                        Path = path,
                        SelectedFormat = filetype,
                        ThumbURL = thumbURL,
                        Title = title,
                        AvailableFormats = formats.ToList()
                    };

                    Videos.Add(video);
                }
            }
            else
            {
                switch (ID.Length)
                {
                    case 0:
                        throw new QueueException(Localization.Strings.InvalidURL);
                    default:
                        Video videoNonYoutube = new Video
                        {
                            ID = ID,
                            Name = filename,
                            Path = path,
                            SelectedFormat = filetype,
                            ThumbURL = thumbURL,
                            Title = title,
                            AvailableFormats = formats.ToList()
                        };

                        Videos.Add(videoNonYoutube);
                        break;
                }
            }
        }
    }
}
