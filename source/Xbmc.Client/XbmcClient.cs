namespace Xbmc.Client
{
    using System;
    using System.Threading;

    using Newtonsoft.Json.Linq;

    using Sysmeta.JsonRpc;

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

        public void Log(string message)
        {
            var request = new JsonRpcRequest
            {
                Credentials = null,
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
                Credentials = null,
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
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.ToggleMute"
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void SetVolume(int volume)
        {
            var request = new JsonRpcRequest()
            {
                Credentials = null,
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
                Credentials = null,
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
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Play"
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        public void Quit()
        {
            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Quit"
            };

            client.Execute<JToken>(request, (response, exception) => {});
        }

        internal int GetRequestId()
        {
            return Interlocked.Increment(ref idCounter);
        }
    }
}