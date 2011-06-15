namespace Sysmeta.Xbmc.Remote.Model
{
    using System;

    using System.Collections.Generic;
    using System.Windows.Media.Imaging;

    using Newtonsoft.Json;

    public class Tvshow
    {
        private static string[] fields;

        static Tvshow()
        {
            fields = new [] { "tvshowid", "title", "genre", "year", "rating", 
                                    "plot", "episode", "playcount", "studio", "mpaa", "premiered" };
        }

        internal static string[] Fields
        {
            get
            {
                return fields;
            }
        }

        [JsonProperty("tvshowid")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }

        [JsonProperty("episode")]
        public int Episode { get; set; }

        [JsonProperty("fanart")]
        public Uri Fanart { get; set; }

        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        [JsonProperty("studio")]
        public string Studio { get; set; }

        [JsonProperty("premiered")]
        public DateTime Premiered { get; set; }

        public BitmapImage ThumbnailImage { get; set; }
    }

    public class TvshowResult
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        public int End { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("tvshows")]
        public IEnumerable<Tvshow> Tvshows { get; set; }
    }
}