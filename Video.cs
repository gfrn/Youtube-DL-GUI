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
        public int filetype { get; set; }
        public string thumbURL { get; set; }
        public string title { get; set; }

        // Stores filetype IDs for Youtube-DL.
        private Dictionary<int, string> fileTypes = new Dictionary<int, string>()
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

        Process ytbDL = new Process
        {
            StartInfo =
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "youtube-dl.exe"
        }
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
                arguments += filetype == 13 ? "" : "-f " + fileTypes[filetype];
                arguments += " -o \"" + path + name + "\"";
                arguments += " " + ID;

                ytbDL.StartInfo.Arguments = arguments;
                ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                (s, f) =>
                {
                    form.SimplifiedStatus = strings.StartingUp;
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
                    MessageBox.Show(strings.InvalidVideo, strings.Error);
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

        public void AbortDownloads()
        {
            ytbDL.Kill();
            form.DownloadButtonText = strings.Download;
        }
    }
}


