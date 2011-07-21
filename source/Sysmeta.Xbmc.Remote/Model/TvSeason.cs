namespace Sysmeta.Xbmc.Remote.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;

    using Newtonsoft.Json;

    public class TvSeason
    {
        private static string[] fields;

        static TvSeason()
        {
            fields = new [] { "title", "genre", "rating", "showtitle",
                                    "season", "episode", "playcount", "studio", "mpaa" };
        }

        public int TvshowId { get; set; }

        public static string[] Fields
        {
            get
            {
                return fields;
            }
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("fanart")]
        public string Fanart { get; set; }

        [JsonProperty("episode")]
        public string Episodes { get; set; }

        public BitmapImage ThumbnailImage { get; set; }
    }

    public class TvSeasonsResult
    {
        public int Start { get; set; }

        public int End { get; set; }

        public int Total { get; set; }

        public IEnumerable<TvSeason> Seasons { get; set; }
    }
}