using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;


namespace WPFMETRO
{
    public class Video
    {
        public string ID { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string SelectedFormat { get; set; }
        public List<KeyValuePair<string, string>> AvailableFormats { get; set; }
        public string ThumbURL { get; set; }
        public string Title { get; set; }

        Process ytbDL = new Process();
        ProcessStartInfo ytbDLInfo = new ProcessStartInfo
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "youtube-dl.exe"
        };

        MainWindow mw = Application.Current.Windows
        .Cast<Window>()
        .FirstOrDefault(window => window is MainWindow) as MainWindow;

        public void DownloadVideo()
        {
            string arguments = "";
            string output = "";
            bool completedDownload = false;
            string progress = "";

            if (!Directory.Exists(Path))
            {
                MessageBox.Show(Localization.Strings.InvalidPath, Localization.Strings.Error);
            }
            else
            {
                if (SelectedFormat != "default")
                {
                    switch (SelectedFormat)
                    {
                        case string s when (s == "mp3" || s == "flac"):
                            arguments += "--prefer-ffmpeg --extract-audio --audio-format " + SelectedFormat;
                            break;
                        default:
                            arguments += "-f ";

                            if (SelectedFormat.Contains(" "))
                            {
                                arguments += SelectedFormat.Substring(0, SelectedFormat.IndexOf(" "));
                                if (SelectedFormat.Substring(SelectedFormat.Length - 1) == "y")
                                {
                                    int spaceIndex = SelectedFormat.IndexOf(" ");
                                    arguments += SelectedFormat.Contains("mp4") ? "+bestaudio[ext!=webm]" : "+bestaudio[ext=webm]‌";
                                    arguments += " --merge-output-format" + SelectedFormat.Substring(spaceIndex, SelectedFormat.Length - spaceIndex - 2);
                                }
                            }
                            else
                            {
                                arguments += SelectedFormat.Substring(0,SelectedFormat.Length-2);
                            }

                            break;
                    }
                }
                arguments += SelectedFormat == "default" ? "-o \"" + Path + Name + "\"" : " -o \"" + Path + Name + "\"";
                arguments += " -i " + ID;

                ytbDLInfo.Arguments = arguments;
                ytbDL = Process.Start(ytbDLInfo);

                ytbDL.OutputDataReceived += new DataReceivedEventHandler(
                (s, f) =>
                {
                    output = f.Data ?? "null";

                    if(output.Contains("has already been downloaded"))
                    {
                        MessageBox.Show(Localization.Strings.AlreadyDownloaded, Localization.Strings.Error);
                        completedDownload = true;
                    }

                    if (output.Contains("[download]") && output.Contains("of"))
                    {
                        if (output.Contains("at") && output.Contains("MiB") && !output.Contains("Destination"))
                        {
                            string downloadSpeed = output.Substring(output.IndexOf("at") + 3, output.IndexOf("ETA") - output.IndexOf("at") - 3);
                            mw.BeginInvoke((Action)(() =>
                            {
                                mw.DownloadSpeed.Text = downloadSpeed;
                            }));
                            progress = output.Substring(output.LastIndexOf("[download]") + 11, output.LastIndexOf("%") - 11);
                            progress = progress.Contains(".") ? progress.Substring(0, progress.IndexOf(".")) : progress;
                        }

                        mw.BeginInvoke((Action)(() =>
                        {
                            mw.DownloadStatus.Text = Localization.Strings.Downloading;
                            mw.DownloadPercentage.Text = progress + "%";
                            mw.DownloadProgressBar.Value = progress != "" ? Int16.Parse(progress) : 0;
                        }));
                        completedDownload = true;
                    }
                    else
                    {
                        mw.BeginInvoke((Action)(() =>
                        {
                            mw.DownloadStatus.Text = output.Contains("[ffmpeg]") ? Localization.Strings.FFMpeg : Localization.Strings.StartingUp;
                        }));
                    }

                    if (output != "null" && Properties.Settings.Default.VerboseStatus)
                    {
                        mw.BeginInvoke((Action)(() =>
                        {
                            mw.VerboseStatus.Text = output;
                        }));
                    }
                }
                );

                ytbDL.Start();
                ytbDL.BeginOutputReadLine();
                ytbDL.WaitForExit();
                ytbDL.CancelOutputRead();

                if (!completedDownload)
                {
                    MessageBox.Show(Localization.Strings.InvalidFiletype, Localization.Strings.Error);
                }

                mw.BeginInvoke((Action)(() =>
                {
                    mw.DownloadSpeed.Text = "0.0 MiB/s";
                    mw.VerboseStatus.Text = "";
                    mw.DownloadPercentage.Text = "0%";
                    mw.ClearCard();
                    mw.DownloadProgressBar.Value = 0;
                    mw.DownloadStatus.Text = Localization.Strings.NoDownload;
                }));

                arguments = "";
            }
        }
    }
}