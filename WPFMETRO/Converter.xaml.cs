using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMETRO;
using System.Diagnostics;

namespace WPFMETRO
{
    /// <summary>
    /// Interaction logic for Converter.xaml
    /// </summary>
    public partial class Converter : MetroWindow
    {
        OpenFileDialog importAVDialog = new OpenFileDialog();
        SaveFileDialog saveAVDialog = new SaveFileDialog();
        OpenFileDialog openSubtitlesDialog = new OpenFileDialog();
        OpenFileDialog openMergeDialog = new OpenFileDialog();

        private Process ffMpegProc = new Process();

        private readonly string videoFilter = "Video (*.m4a;*.mp4;*.3gp;*.m4v;*.mov;*.webm)|*.m4a;*.mp4;*.3gp;*.m4v;*.mov;*.webm";
        private readonly string audioFilter = "Audio (*.ogg;*.mp3;*.m4a;*.flac; *.mpeg) |*.ogg;*.mp3;*.m4a;*.flac;*.mpeg";

        private string[] videoFormats = { ".m4a", ".mp4", ".3gp", ".m4v", ".mov", ".webm" };
        private string[] audioFormats = { ".ogg", ".mp3", ".m4a", ".flac", ".mpeg" };

        private string inputFile = "";
        private string outputFile = "";

        private string videoAudioFilter = "mp4|*.mp4|m4a|*.m4a|3gp|*.3gp|m4v|*.m4v|mov|*.mov|webm|*.webm|ogg|*.ogg|mp3|*.mp3|flac|*.flac|mpeg|*.mpeg";
        private string imageFilter = "png|*.png";
        public Converter()
        {
            InitializeComponent();
            importAVDialog.Filter = videoFilter + "|" + audioFilter;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            JoinVideosButton.IsEnabled = false;

            importAVDialog.ShowDialog();
            if (importAVDialog.CheckFileExists)
            {
                inputFile = importAVDialog.FileName;

                saveAVDialog.Filter = videoAudioFilter + "|" + imageFilter;
                ExportButton.IsEnabled = true;
                OriginalFileLabel.Content = inputFile.Length > 53 ? inputFile.Substring(0, 50) + "..." : inputFile;

                CancelButton.Visibility = Visibility.Visible;
                MergeButton.IsEnabled = AddSubsButton.IsEnabled = true;
                saveAVDialog.FileName = Path.GetFileNameWithoutExtension(importAVDialog.FileName);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            saveAVDialog.ShowDialog();
            if (saveAVDialog.CheckPathExists)
            {
                if (File.Exists(saveAVDialog.FileName))
                {
                    System.Windows.Forms.MessageBox.Show(Localization.Strings.Overwrite, Localization.Strings.Error);
                    saveAVDialog.FileName = "";
                }
                else
                {
                    outputFile = saveAVDialog.FileName;

                    OutputFileLabel.Content = outputFile.Length > 53 ? outputFile.Substring(0, 50) + "..." : outputFile;
                    ConvertFromLabel.Content = Path.GetExtension(inputFile);
                    ConvertToLabel.Content = Path.GetExtension(outputFile);

                    if (Path.GetExtension(outputFile) == ".png")
                    {
                        MergeButton.IsEnabled = AddSubsButton.IsEnabled = false;
                        IntervalSnagBox.IsEnabled = true;
                    }

                    ConvertButton.IsEnabled = true;
                }
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string args = "";
            if (JoinVideosButton.IsEnabled)
            {
                args = "-f concat -i file -list.txt -c copy " + saveAVDialog.FileName;
            }
            else
            {
                args = "-i \"" + inputFile + "\"";
                string output = Path.GetExtension(outputFile) == ".png" ? "\"" + outputFile.Substring(0, outputFile.LastIndexOf('.')) + "_%03d.png\"" : " \"" + saveAVDialog.FileName + "\"";
                if (Path.GetExtension(outputFile) == ".png") { args += " -vf fps=" + IntervalSnagBox.Text + " "; }
                if (CutStartTextbox.Text == "00:00:00.0" && EndOfVideoCheckbox.IsChecked == true)
                {
                    if (openMergeDialog.FileName != "" && openSubtitlesDialog.FileName != "") { System.Windows.Forms.MessageBox.Show(""); }
                    else if (openSubtitlesDialog.FileName != "") { args += "-i " + openSubtitlesDialog.FileName + " - map 0 - map 1 - c copy - c:v"; }
                    else if (openMergeDialog.FileName != "") { args += "-i " + openMergeDialog.FileName + " -c:v copy -c:a aac -strict experimental"; }
                }
                else
                {
                    if (openMergeDialog.FileName != "" || openSubtitlesDialog.FileName != "") { System.Windows.Forms.MessageBox.Show(""); }
                    else
                    {
                        args += " -ss " + CutStartTextbox.Text;
                        args += EndOfVideoCheckbox.IsChecked == true ? "" : " -to " + CutEndTextbox.Text + " ";
                    }
                }
                args += output;
            }

            ffMpegProc.StartInfo.Arguments = args;
            ffMpegProc.StartInfo.UseShellExecute = false;
            ffMpegProc.StartInfo.RedirectStandardOutput = true;
            ffMpegProc.StartInfo.CreateNoWindow = true;
            ffMpegProc.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            ffMpegProc.StartInfo.FileName = "ffmpeg.exe";

            ffMpegProc.EnableRaisingEvents = true;
            ffMpegProc.Exited += new EventHandler(ffMpegProc_Exited);

            ffMpegProc.Start();

            CancelButton.Visibility = CancelJoinButton.Visibility = CancelMergeButton.Visibility = CancelSubsButton.Visibility = Visibility.Hidden;

            EndOfVideoCheckbox.IsEnabled = ConvertButton.IsEnabled = ExportButton.IsEnabled = ImportButton.IsEnabled = false;
            MergeButton.IsEnabled = CutStartTextbox.IsEnabled = EndOfVideoCheckbox.IsEnabled = AddSubsButton.IsEnabled = false;

            StatusLabel.Content = Localization.Strings.FFMpeg;
        }

        private void ffMpegProc_Exited(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() =>
            {
                OriginalFileLabel.Content = "";
                OutputFileLabel.Content = "";
                inputFile = "";
                outputFile = "";
                ConvertFromLabel.Content = "";
                ConvertToLabel.Content = "";

                ImportButton.IsEnabled = CutStartTextbox.IsEnabled = EndOfVideoCheckbox.IsEnabled = JoinVideosButton.IsEnabled = true;

                StatusLabel.Content = Localization.Strings.Done;
            }));
        }

        private void EndOfVideoCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CutEndTextbox.IsEnabled = !EndOfVideoCheckbox.IsChecked == true;
            CutEndTextbox.Text = "0000000";
        }

        private void JoinVideosButton_Click(object sender, EventArgs e)
        {
            ImportButton.IsEnabled = false;
            AddSubsButton.IsEnabled = false;
            IntervalSnagBox.IsEnabled = false;
            MergeButton.IsEnabled = false;
            CancelJoinButton.Visibility = Visibility.Visible;

            /*openVideoListDialog.ShowDialog();

            if (openVideoListDialog.CheckFileExists)
            {
                ExportButton.IsEnabled = true;
            }*/
        }

        private void CancelImportButton_Click(object sender, EventArgs e)
        {
            importAVDialog.FileName = "";
            inputFile = "";

            OriginalFileLabel.Content = "";

            JoinVideosButton.IsEnabled = true;
            CancelButton.Visibility = Visibility.Hidden;
        }

        private void CancelJoinButton_Click(object sender, EventArgs e)
        {
            ConvertButton.IsEnabled = false;
            ImportButton.IsEnabled = true;

            //openVideoListDialog.FileName = ""; Will implement later

            CancelJoinButton.Visibility = Visibility.Hidden;
        }

        private void AddSubsButton_Click(object sender, EventArgs e)
        {
            CancelSubsButton.Visibility = Visibility.Visible;

            openSubtitlesDialog.ShowDialog();

            if (openSubtitlesDialog.CheckFileExists)
            {
                MergeButton.IsEnabled = false;
            }
        }

        private void MergeButton_Click(object sender, EventArgs e)
        {
            if (videoFormats.Any(Path.GetExtension(inputFile).Contains))
            {
                CancelMergeButton.Visibility = Visibility.Visible;
                AddSubsButton.IsEnabled = false;
                openMergeDialog.Filter = videoFilter;
                openMergeDialog.ShowDialog();
            }
            else if (audioFormats.Any(Path.GetExtension(inputFile).Contains))
            {
                AddSubsButton.IsEnabled = false;
                CancelMergeButton.Visibility = Visibility.Visible;
                openMergeDialog.FileName = audioFilter;
                openMergeDialog.ShowDialog();
            }
        }

        private void CancelMergeButton_Click(object sender, EventArgs e)
        {
            openMergeDialog.FileName = "";

            CancelMergeButton.Visibility = Visibility.Hidden;
        }
        private void IntervalSnagBox_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Char keyChar = (Char)System.Text.Encoding.ASCII.GetBytes(e.Text)[0];

            if (Char.IsDigit(keyChar)) return;
            if (Char.IsControl(keyChar)) return;
            if ((keyChar == '.') && ((sender as System.Windows.Controls.TextBox).Text.Contains('.') == false)) return;
            if ((keyChar == '.') && ((sender as System.Windows.Controls.TextBox).SelectionLength == (sender as System.Windows.Controls.TextBox).Text.Length)) return;
            e.Handled = true;
        }
    }
}
