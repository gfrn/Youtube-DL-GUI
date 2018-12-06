using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Net;
using System.IO;

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
    }
}

