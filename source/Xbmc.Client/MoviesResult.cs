namespace Xbmc.Client
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class MoviesResult
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        public int End { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("movies")]
        public IEnumerable<Movie> Movies { get; set; }
    }
}