namespace Sysmeta.Xbmc.Remote.Services
{
    using System;

    using Newtonsoft.Json.Linq;

    using Sysmeta.JsonRpc;
    using Sysmeta.Xbmc.Remote.Model;

    public class VideoLibrary
    {
        private readonly JsonRpcClient client;
        private readonly Func<int> getRequestId;

        internal VideoLibrary(JsonRpcClient client, Func<int> getRequestId)
        {
            this.client = client;
            this.getRequestId = getRequestId;
        }

        public void GetTvshows(Action<TvshowResult, Exception> callback)
        {
            JObject args = new JObject();
            args.Add(new JProperty("fields", Tvshow.Fields));

            JsonRpcRequest request = new JsonRpcRequest
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetTVShows",
                Parameters = args
            };

            this.client.Execute<TvshowResult>(request, (r, e) => callback(r.Result, e));
        }

        public void GetTvEpisodes(int tvshowId, int season, Action<TvEpisodesResult, Exception> callback)
        {
            JObject args = new JObject();
            args.Add(new JProperty("tvshowid", tvshowId));
            args.Add(new JProperty("season", season));

            args.Add(new JProperty("fields", TvEpisode.Fields));

            JsonRpcRequest request = new JsonRpcRequest
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetEpisodes",
                Parameters = args
            };

            this.client.Execute<TvEpisodesResult>(request, (r, e) => callback(r.Result, e));
        }

        public void GetTvSeason(int tvshowId, Action<TvSeasonsResult, Exception> callback)
        {
            JObject args = new JObject();
            args.Add(new JProperty("tvshowid", tvshowId));
            args.Add(new JProperty("fields", TvSeason.Fields));

            JsonRpcRequest request = new JsonRpcRequest
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetSeasons",
                Parameters = args
            };

            this.client.Execute<TvSeasonsResult>(request, (r, e) => callback(r.Result, e));
        }

        public void GetMovies(Action<MoviesResult, Exception> callback)
        {
            var args = new JObject();
            args.Add(new JProperty("fields", Movie.Fields));
            
            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetMovies",
                Parameters = args
            };

            this.client.Execute<MoviesResult>(request, (rpcResponse, exception) => callback(rpcResponse.Result, exception));
        }

        int GetRequestId()
        {
            return this.getRequestId();
        }
    }
}