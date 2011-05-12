using System;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Xbmc.JsonRpc
{
    public enum MovieFields
    {
        Title,
        Genre,
        Year,
        Rating,
        Director,
        Trailer,
        Tagline,
        Plot,
        PlotOutline,
        OriginalTitle,
        LastPlayed,
        PlayCount,
        Writer,
        Studio,
        MPAA,
        Cast,
        Country,
        IMDBNumber,
        Premiered,
        ProductionCode,
        Runtime,
        Set,
        Showlink,
        StreamDetails,
        Top250,
        Votes,
        WritingCredits,
        Fanart,
        Thumbnail,
        File
    }

    public class Movie
    {
        [JsonProperty("movieid")]
        public int MovieId { get; set; }

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

    public class VideoLibrary
    {
        private readonly JsonRpcClient _client;
        private readonly Func<int> _getRequestId;

        internal VideoLibrary(JsonRpcClient client, Func<int> getRequestId)
        {
            _client = client;
            _getRequestId = getRequestId;
        }

        public async Task<MoviesResult> GetMovies(params MovieFields[] fields)
        {
            var param = new JObject();
            if (fields != null && fields.Length > 0)
            {
                JArray jfields = new JArray();
                foreach (var field in fields)
                {
                    jfields.Add(field.ToString());
                }
                param.Add(new JProperty("fields", jfields));
            }

            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetMovies",
                Parameters = param
            };

            var response = await _client.Execute<MoviesResult>(request);

            return response.Result;
        }

        int GetRequestId()
        {
            return _getRequestId();
        }
    }

    public class XbmcClient
    {
        private JsonRpcClient client;
        private int idCounter = 1;

        public XbmcClient(string baseUrl)
        {
            client = new JsonRpcClient(new Uri(baseUrl));

            this.VideoLibrary = new VideoLibrary(client, GetRequestId);
        }

        public VideoLibrary VideoLibrary { get; private set; }

        public async void Log(string message)
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Log",
                Parameters = new JArray { message  }
            };

            await client.Execute<JToken>(request);
        }

        public async void Log(string message, LogLevel level)
        {
            // All the log levels are lowercase characters.
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Log",
                Parameters = new JArray { message, level.ToString().ToLower() }
            };

            await client.Execute<JToken>(request);
        }

        public async void ToggleMute()
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.ToggleMute"
            };

            await client.Execute<JToken>(request);
        }

        public async void SetVolume(int volume)
        {
            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.SetVolume",
                Parameters = new JArray { volume }
            };

            await client.Execute<JToken>(request);
        }

        public async Task<int> GetVolumn()
        {
            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.GetVolume"
            };

            var response = await client.Execute<int>(request);

            return (int)response.Result;
        }

        public async void Play()
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Play"
            };

            await client.Execute<JToken>(request);
        }

        public async void Quit()
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Quit"
            };

            await client.Execute<JToken>(request);
        }

        internal int GetRequestId()
        {
            return Interlocked.Increment(ref idCounter);
        }
    }

    public enum LogLevel
    {
        None,
        Debug,
        Info,
        Notice,
        Warning,
        Error,
        Severe,
        Fatal
    }

    public class Limits
    {
        public Limits()
        {
            Start = 0;
            End = -1;
        }

        public int Start { get; set; }
        public int End { get; set; }
    }

    public enum Order
    {
        Ascending,
        Descending
    }

    public class Sort
	{
        public Order Order { get; set; }
        public bool IgnoreArticle { get; set; }
        public string Method { get; set; }
	}
}