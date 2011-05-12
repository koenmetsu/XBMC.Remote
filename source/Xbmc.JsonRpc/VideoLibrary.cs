namespace Xbmc.JsonRpc
{
    using System;

    using Newtonsoft.Json.Linq;

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
                param.Add(new JProperty("fields", fields));
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
}