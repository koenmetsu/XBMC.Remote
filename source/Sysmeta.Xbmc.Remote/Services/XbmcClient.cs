namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Net;
    using System.Threading;

    using Newtonsoft.Json.Linq;

    using Sysmeta.JsonRpc;
    using Sysmeta.Xbmc.Remote.Model;

    public class XbmcClient : IXbmcClient
    {
        private readonly JsonRpcClient client;
        private int idCounter = 1;

        private ICredentials credentials;

        public XbmcClient(string baseUrl, string username, string password, bool executeCallbackOnUIThread = true)
        {
            client = new JsonRpcClient(this.BuildUrl(baseUrl), executeCallbackOnUIThread);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                this.credentials = new NetworkCredential(username, password);
            }

            this.Video = new VideoLibrary(client, GetRequestId, this.credentials);
            this.Vfs = new Vfs(new Uri(baseUrl), this.credentials, executeCallbackOnUIThread);
        }

        public IVideoLibrary Video { get; private set; }

        public IVfs Vfs { get; private set; }

        public void Log(string message)
        {
            var request = new JsonRpcRequest
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.Log",
                Parameters = new JArray { message  }
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void Log(string message, LogLevel level)
        {
            // All the log levels are lowercase characters.
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.Log",
                Parameters = new JArray { message, level.ToString().ToLower() }
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void ToggleMute()
        {
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.ToggleMute"
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void SetVolume(int volume)
        {
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.SetVolume",
                Parameters = new JArray { volume }
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void GetVolumn(Action<int, Exception> callback)
        {
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.GetVolume"
            };

            client.Execute<int>(request, (rpcResponse, exception) =>
                {
                    if (exception != null)
                    {
                        callback(0, exception);
                    }
                    else
                    {
                        callback(rpcResponse.Result, null);
                    }
                });

            return ;
        }

        public void Play()
        {
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.Play"
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void PlayMovie(int id)
        {
            JObject args = new JObject();
            args.Add(new JProperty("movieid", id));

            var request = new JsonRpcRequest
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.Play",
                Parameters = args
            };

            client.Execute<JToken>(request, (response, exception) => { });
        }

        public void PlayEpisode(int id)
        {
            JObject args = new JObject();
            args.Add(new JProperty("episodeid", id));

            var request = new JsonRpcRequest
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.Play",
                Parameters = args
            };

            client.Execute<JToken>(request, (response, exception) => { });
        }

        public void Quit()
        {
            var request = new JsonRpcRequest()
            {
                Credentials = this.credentials,
                Id = GetRequestId(),
                Method = "XBMC.Quit"
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        internal int GetRequestId()
        {
            return Interlocked.Increment(ref idCounter);
        }

        private Uri BuildUrl(string baseUrl)
        {
            var builder = new UriBuilder(baseUrl);
            builder.Path = "/jsonrpc";

            return builder.Uri;
        }
    }
}