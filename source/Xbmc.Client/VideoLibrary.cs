namespace Xbmc.Client
{
    using System;

    using Newtonsoft.Json.Linq;

    using Sysmeta.JsonRpc;

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

        int GetRequestId()
        {
            return _getRequestId();
        }
    }
}