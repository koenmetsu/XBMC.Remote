namespace Xbmc.Client
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
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
}