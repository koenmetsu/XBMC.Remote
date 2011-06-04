namespace Sysmeta.Xbmc.Remote.Services
{
    using System;

    using Newtonsoft.Json.Linq;

    using Sysmeta.JsonRpc;
    using Sysmeta.Xbmc.Remote.Model;

    public class VideoLibrary
    {
        private readonly JsonRpcClient _client;
        private readonly Func<int> _getRequestId;

        internal VideoLibrary(JsonRpcClient client, Func<int> getRequestId)
        {
            _client = client;
            _getRequestId = getRequestId;
        }

        public void GetMovies(Action<MoviesResult, Exception> callback, params MovieFields[] fields)
        {
            var param = new JObject();
            if (fields != null && fields.Length > 0)
            {
                var jfields = new JArray();
                foreach (var field in fields)
                {
                    jfields.Add(field.ToString());
                }
                jfields.Add("imdbnumber");
                param.Add(new JProperty("fields", jfields));
            }

            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "VideoLibrary.GetMovies",
                Parameters = param
            };

            _client.Execute<MoviesResult>(request, (rpcResponse, exception) => callback(rpcResponse.Result, exception));
        }

        public void GetMovieDetails(Action<Movie, Exception> callback, int movieId, params MovieFields[] fields)
        {
            var param = new JObject();

            if (fields != null && fields.Length > 0)
            {
                var jfields = new JArray();
                foreach (var field in fields)
                {
                    jfields.Add(field.ToString().ToLower());
                }
                param.Add(new JProperty("fields", jfields));
            }

            param.Add(new JProperty("movieid", movieId));

            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Version = "2.0",
                Id = GetRequestId(),
                Method = "VideoLibrary.GetMovieDetails",
                Parameters = param
            };

            _client.Execute<MoviesResult>(request, (rpcResponse, exception) =>
                {
                    callback(null, exception);
                });
        }

        int GetRequestId()
        {
            return _getRequestId();
        }
    }
}