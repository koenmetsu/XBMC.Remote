using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    public class TvshowDesignData
    {
        public TvshowDesignData()
        {
            this.Title = "Bones";
            this.Episodes = 19;
            this.Thumbnail = new BitmapImage() { UriSource = new Uri(
                @"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/c/c9e0aea1.tbn") };
        }

        public string Title { get; set; }
        public BitmapImage Thumbnail { get; set; }
        public string Plot { get; set; }
        public string Genre { get; set; }
        public float Rating { get; set; }
        public int Episodes { get; set; }
        public BitmapImage Fanart { get; set; }
        public int PlayCount { get; set; }
        public string Studio { get; set; }
        public string Premiered { get; set; }
    }
}