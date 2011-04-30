using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Xbmc.Http;
using System.Threading.Tasks;

namespace Xbmc.JsonRpc
{
    public class JsonRpcRequest
    {
        /// <summary>
        /// In general you would not need to set this directly. Used by the NtlmAuthenticator. 
        /// </summary>
        [JsonIgnore]
        public ICredentials Credentials { get; set; }

        [JsonProperty("jsonrpc")]
        public string Version { get; set; }

        /// <summary>
        /// Containing the name of the method to be invoked.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// List of objects to pass as arguments to the method
        /// </summary>
        [JsonProperty("params", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<object> Parameters { get; set; }

        /// <summary>
        /// The request id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class JsonRpcResponse
    {
        /// <summary>
        /// The request id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("jsonrpc")]
        public string Version { get; set; }
    }

    public class JsonRpcClient
    {
        private Uri baseUrl;

        public JsonRpcClient(Uri baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public Uri BaseUrl
        {
            get { return this.baseUrl; }
            set { this.baseUrl = value; }
        }

        public async Task<JsonRpcResponse> Execute(JsonRpcRequest request)
        {
            var http = new HttpClient();
            ConfigureHttp(request, http);

            var repsonse = await http.Post();

            return ConvertToJsonRpcResponse(repsonse);
        }

        private void ConfigureHttp(JsonRpcRequest request, HttpClient http)
        {
            // Only support this version
            request.Version = "2.0";

            http.Url = BaseUrl;

            if (request.Credentials != null)
            {
                http.Credentials = request.Credentials;
            }

            http.RequestBody = JsonConvert.SerializeObject(request);
            http.RequestContentType = "application/json-rpc";
        }

        private JsonRpcResponse ConvertToJsonRpcResponse(HttpResponse httpResponse)
        {
            return JsonConvert.DeserializeObject<JsonRpcResponse>(httpResponse.Content);
        }
    }
}