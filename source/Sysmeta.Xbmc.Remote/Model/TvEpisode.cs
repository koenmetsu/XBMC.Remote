namespace Sysmeta.Xbmc.Remote.Model
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class TvEpisode
    {
        private static string[] fields;

        static TvEpisode()
        {
            fields = new string[] { "title", "year", "rating", 
                                    "director", "plot", "lastplayed", "showtitle", "firstaired", 
                                    "duration", "season", "episode", "playcount", "writer",
                                    "studio", "mpaa", "premiered", "episodeid" };
        }

        public static string[] Fields
        {
            get
            {
                return fields;
            }
        }

        [JsonProperty("episodeid")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("lastplayed")]
        public string LastPlayed { get; set; }

        [JsonProperty("showtitle")]
        public string ShowTitle { get; set; }

        [JsonProperty("firstaired")]
        public string FirstAired { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("episode")]
        public string Episode { get; set; }

        [JsonProperty("playcount")]
        public string PlayCount { get; set; }

        [JsonProperty("writer")]
        public string Writer { get; set; }

        [JsonProperty("studio")]
        public string Studio { get; set; }

        [JsonProperty("mpaa")]
        public string MPAA { get; set; }

        [JsonProperty("premiered")]
        public string Premiered { get; set; }

        [JsonProperty("fanart")]
        public string Fanart { get; set; }
    }

    public class TvEpisodesResult
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        public int End { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("episodes")]
        public IEnumerable<TvEpisode> Episodes { get; set; }
    }
}