namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.IO;
    using System.Net;

    using Sysmeta.JsonRpc;

    public class Vfs : IVfs
    {
        private Uri baseUrl;

        private ICredentials credentials;

        private readonly bool executeCallbackOnUiThread;

        public Vfs(Uri url, ICredentials credentials, bool executeCallbackOnUIThread = true)
        {
            this.baseUrl = url;
            this.credentials = credentials;
            executeCallbackOnUiThread = executeCallbackOnUIThread;
        }

        public void GetFile(Uri uri, Action<byte[], Exception> callback)
        {
            var client = new HttpClient(this.executeCallbackOnUiThread) { Credentials = this.credentials };
            client.Url = BuildUrl(uri);
            
            client.Get(response =>
                {
                    if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        callback(null, response.ErrorException);
                    }
                    else
                    {
                        callback(response.RawBytes, null);
                    }
                });
        }

        private Uri BuildUrl(Uri uri)
        {
            var builder = new UriBuilder(this.baseUrl);
            builder.Path = "/vfs/" + uri.ToString();

            return builder.Uri;
        }
    }
}