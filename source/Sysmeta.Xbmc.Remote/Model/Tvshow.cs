namespace Sysmeta.Xbmc.Remote.Model
{
    using System;

    using Newtonsoft.Json;

    public class Tvshow
    {
        [JsonProperty("tvshowid")]
        public int Id { get; set; }

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
        public double Rating { get; set; }
    }
}