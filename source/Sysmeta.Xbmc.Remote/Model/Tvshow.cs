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
        public string Year { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("episode")]
        public string Episode { get; set; }

        [JsonProperty("fanart")]
        public string Fanart { get; set; }

        [JsonProperty("playcount")]
        public string PlayCount { get; set; }

        [JsonProperty("studio")]
        public string Studio { get; set; }

        [JsonProperty("premiered")]
        public string Premiered { get; set; }

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