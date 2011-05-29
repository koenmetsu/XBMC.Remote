namespace Sysmeta.Xbmc.Client
{
    using System;

    using Sysmeta.JsonRpc;

    public class Vfs
    {
        private Uri baseUrl;

        private readonly bool executeCallbackOnUiThread;

        public Vfs(Uri url, bool executeCallbackOnUIThread = true)
        {
            this.baseUrl = url;
            executeCallbackOnUiThread = executeCallbackOnUIThread;
        }

        public void GetFile(Uri uri, Action<byte[], Exception> callback)
        {
            var client = new HttpClient(this.executeCallbackOnUiThread);
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