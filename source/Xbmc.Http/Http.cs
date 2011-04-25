using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Xbmc.Http
{
    public class Http
    {
        readonly IDictionary<string, Action<HttpWebRequest, string>> _restrictedHeaderActions;

        public Http()
        {
            Headers = new List<HttpHeader>();
            Cookies = new List<HttpCookie>();
            Files = new List<HttpFile>();
            Parameters = new List<HttpParameter>();

            _restrictedHeaderActions = new Dictionary<string, Action<HttpWebRequest, string>>(StringComparer.OrdinalIgnoreCase);

            AddSharedHeaderActions();
            AddAsyncHeaderActions();
            AddSyncHeaderActions();
        }

        /// <summary>
        /// HTTP headers that will be sent with the request
        /// </summary>
        public IList<HttpHeader> Headers { get; private set; }

        /// <summary>
        /// HTTP cookies to be sent with request
        /// </summary>
        public IList<HttpCookie> Cookies { get; private set; }

        /// <summary>
        /// System.Net.ICredentials to be sent with request
        /// </summary>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// UserAgent to be sent with request
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Whether or not HTTP 3xx response redirects should be automatically followed
        /// </summary>
        public bool FollowRedirects { get; set; }

        /// <summary>
        /// URL to call for this request
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Collection of files to be sent with request
        /// </summary>
        public IList<HttpFile> Files { get; private set; }

        /// <summary>
        /// True if files have been set to be uploaded
        /// </summary>
        protected bool HasFiles
        {
            get
            {
                return Files.Any();
            }
        }

        /// <summary>
        /// HTTP parameters (QueryString or Form values) to be sent with request
        /// </summary>
        public IList<HttpParameter> Parameters { get; private set; }

        /// <summary>
        /// Request body to be sent with request
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// True if this HTTP request has any HTTP parameters
        /// </summary>
        protected bool HasParameters
        {
            get
            {
                return Parameters.Any();
            }
        }

        /// <summary>
        /// True if a request body has been specified
        /// </summary>
        protected bool HasBody
        {
            get
            {
                return !string.IsNullOrEmpty(RequestBody);
            }
        }

        /// <summary>
        /// Content type of the request body.
        /// </summary>
        public string RequestContentType { get; set; }

        void AddSharedHeaderActions()
        {
            _restrictedHeaderActions.Add("Accept", (r, v) => r.Accept = v);
            _restrictedHeaderActions.Add("Content-Type", (r, v) => r.ContentType = v);
            _restrictedHeaderActions.Add("Date", (r, v) => { /* Set by system */ });
            _restrictedHeaderActions.Add("Host", (r, v) => { /* Set by system */ });
            _restrictedHeaderActions.Add("Range", (r, v) => { /* Ignore */ });
        }

        void AddAsyncHeaderActions()
        {
            // WP7 doesn't as of Beta doesn't support a way to set Content-Length either directly
            // or indirectly
            _restrictedHeaderActions.Add("Content-Length", (r, v) => { });
        }

        void AddSyncHeaderActions()
        {
            _restrictedHeaderActions.Add("User-Agent", (r, v) => r.UserAgent = v);
        }
    }
}