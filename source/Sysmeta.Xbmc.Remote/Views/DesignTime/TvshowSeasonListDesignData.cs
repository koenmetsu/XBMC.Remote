namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;

    public class TvshowSeasonsDesignData
    {
        public TvshowSeasonsDesignData()
        {
            this.Title = "Bones";
            this.Plot = "A forensic anthropologist and a cocky FBI agent build a team to investigate death causes. And quite often, there isn't more to examine than rotten flesh or mere bones. ";
        }

        public string Title { get; set; }

        public string Plot { get; set; }
    }

    public class TvshowSeasonListDesignData
    {
        public TvshowSeasonListDesignData()
        {
            this.Seasons = new[]
                {
                    new TvshowSeasonDesignData()
                        {
                            Title = "Season 1",
                            Thumbnail =
                                new BitmapImage()
                                    { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/b/b8edf222.tbn") }
                        },
                    new TvshowSeasonDesignData()
                        {
                            Title = "Season 3",
                            Thumbnail =
                                new BitmapImage()
                                    { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/b/b16fc94c.tbn") }
                        },
                    new TvshowSeasonDesignData()
                        {
                            Title = "Season 4",
                            Thumbnail =
                                new BitmapImage()
                                    { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/a/af289949.tbn") }
                        },
                    new TvshowSeasonDesignData()
                        {
                            Title = "Season 5",
                            Thumbnail =
                                new BitmapImage()
                                    { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/a/abe984fe.tbn") }
                        },
                    new TvshowSeasonDesignData()
                        {
                            Title = "Season 6",
                            Thumbnail =
                                new BitmapImage()
                                    { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/a/a6aaa227.tbn") }
                        },
                };
        }

        public IEnumerable<TvshowSeasonDesignData> Seasons { get; set; }
    }

    public class TvshowSeasonDesignData
    {
        public TvshowSeasonDesignData()
        {
            this.Title = "Season 1";
            this.Thumbnail = new BitmapImage()
            {
                UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/b/b8edf222.tbn")
            };
        }

        public string Title { get; set; }

        public BitmapImage Thumbnail { get; set; }
    }
}