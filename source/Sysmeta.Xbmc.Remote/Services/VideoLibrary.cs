namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Net;

    using Newtonsoft.Json.Linq;

    using Sysmeta.JsonRpc;
    using Sysmeta.Xbmc.Remote.Model;

    public class VideoLibrary : IVideoLibrary
    {
        private readonly JsonRpcClient client;
        private readonly Func<int> getRequestId;

        private readonly ICredentials credentials;

        internal VideoLibrary(JsonRpcClient client, Func<int> getRequestId, ICredentials credentials)
        {
            this.client = client;
            this.getRequestId = getRequestId;
            this.credentials = credentials;
        }

        public void GetTvshows(Action<TvshowResult, Exception> callback)
        {
            JObject args = new JObject();
            args.Add(new JProperty("fields", Tvshow.Fields));

            var request = new JsonRpcRequest
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetTVShows",
                Parameters = args
            };

            this.client.Execute<TvshowResult>(request, (r, e) =>
                {
                    if (r == null)
                    {
                        callback(null, e);
                    }
                    else
                    {
                        callback(r.Result, e);
                    }
                });
        }

        public void GetTvEpisodes(int tvshowId, int season, Action<TvEpisodesResult, Exception> callback)
        {
            JObject args = new JObject();
            args.Add(new JProperty("tvshowid", tvshowId));
            args.Add(new JProperty("season", season));

            args.Add(new JProperty("fields", TvEpisode.Fields));

            var request = new JsonRpcRequest
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetEpisodes",
                Parameters = args
            };

            this.client.Execute<TvEpisodesResult>(request, (r, e) =>
                {
                    if (r == null)
                    {
                        callback(null, e);
                    }
                    else
                    {
                        callback(r.Result, e);
                    }
                });
        }

        public void GetTvSeason(int tvshowId, Action<TvSeasonsResult, Exception> callback)
        {
            JObject args = new JObject();
            args.Add(new JProperty("tvshowid", tvshowId));
            args.Add(new JProperty("fields", TvSeason.Fields));

            var request = new JsonRpcRequest
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetSeasons",
                Parameters = args
            };

            this.client.Execute<TvSeasonsResult>(request, (r, e) =>
                {
                    if (r == null)
                    {
                        callback(null, e);
                    }
                    else
                    {
                        callback(r.Result, e);
                    }
                });
        }

        public void GetMovies(Action<MoviesResult, Exception> callback)
        {
            var args = new JObject();
            args.Add(new JProperty("fields", Movie.Fields));
            
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetMovies",
                Parameters = args
            };

            this.client.Execute<MoviesResult>(request, (rpcResponse, exception) =>
                {
                    if (rpcResponse == null)
                    {
                        callback(null, exception);
                    }
                    else
                    {
                        callback(rpcResponse.Result, exception);
                    }
                });
        }

        public void GetRecentlyAddedMovies(Action<MoviesResult, Exception> callback)
        {
            var args = new JObject();
            args.Add(new JProperty("fields", Movie.Fields));
            args.Add(new JProperty("start", 0));
            args.Add(new JProperty("end", 6));

            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetRecentlyAddedMovies",
                Parameters = args
            };

            this.client.Execute<MoviesResult>(request, (rpcResponse, exception) =>
                {
                    if (rpcResponse == null)
                    {
                        callback(null, exception);
                    }
                    else
                    {
                        callback(rpcResponse.Result, exception);
                    }
                });            
        }

        int GetRequestId()
        {
            return this.getRequestId();
        }
    }
}