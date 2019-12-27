using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.IO;
using youtube_dl.Properties;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace youtube_dl
{
    public class Video
    {
        public string ID { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public string filetype { get; set; }
        public string filetypeDesc { get; set; }
        public string thumbURL { get; set; }
        public string title { get; set; }

        Process ytbDL = new Process();

        ProcessStartInfo ytbDLInfo = new ProcessStartInfo
        {

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "youtube-dl.exe"
        };

        private readonly Form1 form;
        public Video(Form1 form)
        {
            this.form = form;
        }


        public Image GetThumbnail()
        {
            using (WebClient client = new WebClient())
            {
                if (this.thumbURL != null)
                {
                    byte[] bytes = client.DownloadData(this.thumbURL);
                    MemoryStream ms = new MemoryStream(bytes);

                    Image thumbnail = Image.FromStream(ms);

                    return thumbnail;
                }
                else
                {
                    return Properties.Resources.noThumb;
                }
            }
        }

        public void DownloadVideo()
        {
            string arguments = "";
            string output = "";
            bool completedDownload = false;
            string progress = "";

            if (!Directory.Exists(path))
            {
                MessageBox.Show(strings.InvalidPath, strings.Error);
            }
            else
            {
                if(filetype != "default")
                {
                    switch (filetype)
                    {
                        case string s when (s == "mp3" || s == "flac"):
                            arguments += "--prefer-ffmpeg --extract-audio --audio-format " + filetype;
                            break;
                        default:
                            arguments += "-f ";

                            if (filetype.Contains(" "))
                            {
                                arguments += filetype.Substring(0, filetype.IndexOf(" "));
                                arguments += filetype.Contains("mp4") ? "+bestaudio[ext!=webm]" : "+bestaudio[ext=webm]‌";
                                arguments += " --merge-output-format" + filetype.Substring(filetype.IndexOf(" "));
                            }
                            else
                            {
                                 arguments += filetype;
                            }
                            
                            break;
                    }
                }
                arguments += filetype == "default" ? "-o \"" + path + name + "\"" : " -o \"" + path + name + "\"";
                arguments += " " + ID;

                ytbDLInfo.Arguments = arguments;
                ytbDL = Process.Start(ytbDLInfo);

                ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                (s, f) =>
                {
                    output = f.Data ?? "null";

                    if (output.Contains("[download]") && output.Contains("of"))
                    {
                        if (output.Contains("at") && output.Contains("MiB") && !output.Contains("Destination"))
                        {
                            string downloadSpeed = output.Substring(output.IndexOf("at") + 3, output.IndexOf("ETA") - output.IndexOf("at") - 3);
                            form.DownloadSpeed = downloadSpeed;
                            progress = output.Substring(output.LastIndexOf("[download]") + 11, output.LastIndexOf("%") - 11);
                            progress = progress.Contains(".") ? progress.Substring(0, progress.IndexOf(".")) : progress;
                        }
                        form.SimplifiedStatus = strings.Downloading;
                        form.BeginInvoke((Action)(() =>
                        {
                            form.DownloadProgress = progress != "" ? Int16.Parse(progress) : 0;
                        }));
                        completedDownload = true;
                    }
                    else
                    {
                        form.SimplifiedStatus = output.Contains("[ffmpeg]") ? strings.FFMpeg : strings.StartingUp;
                    }

                    if (output != "null" && form.DisplayVerbose)
                    {
                        form.VerboseStatus = output;
                    }
                }
                );

                ytbDL.Start();
                ytbDL.BeginOutputReadLine();
                ytbDL.WaitForExit();
                ytbDL.CancelOutputRead();

                if (!completedDownload)
                {
                    MessageBox.Show(strings.InvalidFiletype, strings.Error);
                }

                form.BeginInvoke((Action)(() =>
                {
                    form.DownloadSpeed = "0.0 MiB/s";
                    form.VerboseStatus = "";
                    form.ClearCard();
                    form.DownloadProgress = 0;
                    form.DownloadGridView.Rows.RemoveAt(0);
                    form.SimplifiedStatus = strings.NoDownload;
                }));

                arguments = "";
            }
        }
    }
}


