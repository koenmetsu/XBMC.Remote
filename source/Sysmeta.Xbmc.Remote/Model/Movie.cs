namespace Sysmeta.Xbmc.Remote.Model
{
    using System;

    using Newtonsoft.Json;

    public class Movie
    {
        private static string[] _fields;

        static Movie()
        {
            _fields = new string[] { "title", "genre", "year", "rating", "director", "file",
                                    "trailer", "tagline", "plot", "plotoutline", "originaltitle", 
                                    "lastplayed", "duration", "playcount", "writer", "studio", 
                                    "mpaa", "movieid" };
        }

        internal static string[] Fields
        {
            get
            {
                return _fields;
            }

        }

        [JsonProperty("movieid")]
        public int Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("trailer")]
        public string Trailer { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("plotoutline")]
        public string PlotOutline { get; set; }

        [JsonProperty("originaltitle")]
        public string OriginalTitle { get; set; }

        [JsonProperty("lastplayed")]
        public string LastPlayed { get; set; }

        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        [JsonProperty("writer")]
        public string Writer { get; set; }

        [JsonProperty("studio")]
        public string Studio { get; set; }

        [JsonProperty("mpaa")]
        public string MPAA { get; set; }

        [JsonProperty("cast")]
        public string Cast { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("imdbnumber")]
        public string IMDBNumber { get; set; }

        [JsonProperty("premiered")]
        public string Premiered { get; set; }

        [JsonProperty("productioncode")]
        public string ProductionCode { get; set; }

        [JsonProperty("runtime")]
        public string Runtime { get; set; }

        [JsonProperty("set")]
        public string Set { get; set; }

        [JsonProperty("showlink")]
        public string Showlink { get; set; }

        [JsonProperty("streamdetails")]
        public string StreamDetails { get; set; }

        [JsonProperty("top250")]
        public string TOP250 { get; set; }

        [JsonProperty("votes")]
        public string Votes { get; set; }

        [JsonProperty("writingcredits")]
        public string WritingCredits { get; set; }

        [JsonProperty("fanart")]
        public Uri Fanart { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }
    }
}