using System;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Xbmc.JsonRpc
{
    public class XbmcClient
    {
        private JsonRpcClient client;
        private int idCounter = 1;

        public XbmcClient(string baseUrl)
        {
            client = new JsonRpcClient(new Uri(baseUrl));
        }

        public async void Log(string message)
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Log",
                Parameters = new[] { message }
            };

            await client.Execute(request);
        }

        public async void Log(string message, LogLevel level)
        {
            // All the log levels are lowercase characters.
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Log",
                Parameters = new[] { message, level.ToString().ToLower() }
            };

            await client.Execute(request);
        }

        public async void ToggleMute()
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.ToggleMute"
            };

            await client.Execute(request);
        }

        public async void SetVolume(int volume)
        {
            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.SetVolume",
                Parameters = new object[] { volume }
            };

            await client.Execute(request);
        }

        public async Task<int> GetVolumn(Action<int> callback)
        {
            var request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.GetVolume"
            };

            var response = await client.Execute(request);

            return JsonConvert.DeserializeObject<int>(response.Result);
        }

        public async void Play()
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Play"
            };

            await client.Execute(request);
        }

        public async void Quit()
        {
            JsonRpcRequest request = new JsonRpcRequest()
            {
                Credentials = null,
                Id = GetRequestId(),
                Method = "XBMC.Quit"
            };

            await client.Execute(request);
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
}