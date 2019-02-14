using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private string outputFilter = "";

        private string videoAudioFormats = "mp4|*.mp4|m4a|*.m4a|3gp|*.3gp|m4v|*.m4v|mov|*.mov|webm|*.webm|ogg|*.ogg|mp3|*.mp3|flac|*.flac";

        public Converter()
        {
            InitializeComponent();
            openFileDialog.Filter = videoFilter + "|" + audioFilter;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            if(openFileDialog.CheckFileExists)
            {
                //string fileformat = Path.GetExtension(openFileDialog.FileName); Will add when sorting filetypes becomes necessary
                saveFileDialog.Filter = videoAudioFormats;
            }
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }
    }
}
