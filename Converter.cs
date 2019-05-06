using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtube_dl
{
    public partial class Converter : Form
    {
        private string videoFilter = "Video Files (*.m4a;*.mp4;*.3gp;*.m4v;*.mov;*.webm)|*.m4a;*.mp4;*.3gp;*.m4v;*.mov;*.webm";
        private string audioFilter = "Audio Files (*.ogg;*.mp3;*.m4a;*.flac) |*.ogg;*.mp3;*.m4a;*.flac";

        private string[] videoFormats = { ".m4a", ".mp4", ".3gp", ".m4v", ".mov", ".webm" };
        private string[] audioFormats = { ".ogg", ".mp3", ".m4a", ".flac" };

        private string inputFile = "";
        private string outputFile = "";

        private string videoAudioFilter = "mp4|*.mp4|m4a|*.m4a|3gp|*.3gp|m4v|*.m4v|mov|*.mov|webm|*.webm|ogg|*.ogg|mp3|*.mp3|flac|*.flac";
        private string imageFilter = "png|*.png";

        ProcessStartInfo ffmpegInfo = new ProcessStartInfo
        {

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            FileName = "ffmpeg.exe"
        };

        public Converter()
        {
            InitializeComponent();
            openFileDialog.Filter = videoFilter + "|" + audioFilter;
            statusLabel.Text = Properties.strings.NoConversion;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = Properties.strings.NoConversion;
            JoinVideosButton.Enabled = false;

            openFileDialog.ShowDialog();
            if(openFileDialog.CheckFileExists)
            {
                inputFile = openFileDialog.FileName;

                saveFileDialog.Filter = videoAudioFilter + "|" + imageFilter;
                SaveFileButton.Enabled = true;
                OriginalFileLabel.Text = inputFile.Length > 53 ? inputFile.Substring(0, 50) + "..." : inputFile;

                CancelImportButton.Visible = MergeButton.Enabled = AddSubsButton.Enabled = true;
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
            }
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            if(saveFileDialog.CheckPathExists)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    MessageBox.Show(Properties.strings.Overwrite, Properties.strings.Error);
                    saveFileDialog.FileName = "";
                }
                else
                {
                    outputFile = saveFileDialog.FileName;

                    OutputFileLabel.Text = outputFile.Length > 53 ? outputFile.Substring(0, 50) + "..." : outputFile;
                    ConvertFromToLabel.Text = Path.GetExtension(inputFile) + "/" + Path.GetExtension(outputFile);

                    if (Path.GetExtension(outputFile) == ".png")
                    {
                        MergeButton.Enabled = AddSubsButton.Enabled = IntervalSnagBox.Enabled = false;
                    }

                    ConvertButton.Enabled = true;
                }
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            string args = "";
            if (JoinVideosButton.Enabled)
            {
                args = "-f concat -i file -list.txt -c copy " + saveFileDialog.FileName;
            }
            else
            {
                args = "-i \"" + inputFile + "\"";
                if (Path.GetExtension(outputFile) == ".png") { args += " -r " + IntervalSnagBox.Text + " " + outputFile.Substring(0, outputFile.LastIndexOf('.')) + "_%04d.png"; }
                else if (CutStartTextbox.Text == "00:00:00.0" && EndOfVideoCheckbox.Checked)
                {
                    if (openMergeDialog.FileName != "" && openSubtitlesDialog.FileName != "") { MessageBox.Show(""); }
                    else if (openSubtitlesDialog.FileName != "") { args += "-i " + openSubtitlesDialog.FileName + " - map 0 - map 1 - c copy - c:v"; }
                    else if (openMergeDialog.FileName != "") { args += "-i " + openMergeDialog.FileName + " -c:v copy -c:a aac -strict experimental"; }

                    args += " " + "\"" + outputFile + "\"";
                }
                else
                {
                    if (openMergeDialog.FileName != "" || openSubtitlesDialog.FileName != "") { MessageBox.Show(""); }
                    else
                    {
                        args += " -ss " + CutStartTextbox.Text;
                        args += EndOfVideoCheckbox.Checked ? "" : " -to " + CutEndTextbox.Text;
                    }

                    args += " " + "\"" + saveFileDialog.FileName + "\"";
                }
            }

            ffmpegInfo.Arguments = args;
            Process ffmpegProc = Process.Start(ffmpegInfo);

            ffmpegProc.OutputDataReceived += new DataReceivedEventHandler(
                (s, f) =>
                {
                    AddSubsButton.Enabled = CancelImportButton.Visible = CancelJoinButton.Visible = CancelMergeButton.Visible = CancelSubtitlesButton.Visible = false;
                    EndOfVideoCheckbox.Enabled = ConvertButton.Enabled = SaveFileButton.Enabled = OpenFileButton.Enabled = MergeButton.Enabled = false;

                    statusLabel.Text = Properties.strings.FFMpeg;
                });

            statusLabel.Text = Properties.strings.Done;

            OriginalFileLabel.Text = "";
            OutputFileLabel.Text = "";
            inputFile = "";
            outputFile = "";
            ConvertFromToLabel.Text = "";

            ConvertButton.Enabled = false;
        }

        private void EndOfVideoCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CutEndTextbox.Enabled = !EndOfVideoCheckbox.Checked;
            CutEndTextbox.Text = "0000000";
        }

        private void JoinVideosButton_Click(object sender, EventArgs e)
        {
            OpenFileButton.Enabled = false;
            AddSubsButton.Enabled = false;
            IntervalSnagBox.Enabled = false;
            MergeButton.Enabled = false;
            CancelJoinButton.Visible = true;

            openVideoListDialog.ShowDialog();

            if(openVideoListDialog.CheckFileExists)
            {
                SaveFileButton.Enabled = true;
            }
        }

        private void CancelImportButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            inputFile = "";

            JoinVideosButton.Enabled = true;
            CancelImportButton.Visible = false;
        }

        private void CancelJoinButton_Click(object sender, EventArgs e)
        {
            ConvertButton.Enabled = false;
            OpenFileButton.Enabled = true;

            openVideoListDialog.FileName = "";

            CancelJoinButton.Visible = false;
        }

        private void AddSubsButton_Click(object sender, EventArgs e)
        {
            CancelSubtitlesButton.Visible = true;

            openSubtitlesDialog.ShowDialog();

            if(openSubtitlesDialog.CheckFileExists)
            {
                MergeButton.Enabled = false;
            }
        }

        private void MergeButton_Click(object sender, EventArgs e)
        {
            if(videoFormats.Any(Path.GetExtension(inputFile).Contains))
            {
                CancelMergeButton.Visible = true;
                AddSubsButton.Enabled = false;
                openMergeDialog.Filter = videoFilter;
                openMergeDialog.ShowDialog();
            }
            else if (audioFormats.Any(Path.GetExtension(inputFile).Contains))
            {
                AddSubsButton.Enabled = false;
                CancelMergeButton.Visible = true;
                openMergeDialog.FileName = audioFilter;
                openMergeDialog.ShowDialog();
            }
        }

        private void CancelMergeButton_Click(object sender, EventArgs e)
        {
            openMergeDialog.FileName = "";

            CancelMergeButton.Visible = false;
        }

        private void IntervalSnagBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains('.') == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;
            e.Handled = true;
        }
    }
}
