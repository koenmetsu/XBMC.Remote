namespace Sysmeta.JsonRpc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Browser;
    using System.Text;
    using System.Windows;

    using Sysmeta.JsonRpc.Extensions;

    public class HttpClient
    {
        readonly IDictionary<string, Action<HttpWebRequest, string>> _restrictedHeaderActions;

        public HttpClient()
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

        public void Delete(Action<HttpResponse> callback)
        {
            Invoke("DELETE", callback);
        }

        public void Get(Action<HttpResponse> callback)
        {
            Invoke("GET", callback);
        }

        public void Head(Action<HttpResponse> callback)
        {
            Invoke("HEAD", callback);
        }

        public void Options(Action<HttpResponse> callback)
        {
            Invoke("OPTIONS", callback);
        }

        public void Post(Action<HttpResponse> callback)
        {
            PutPost("POST", callback);
        }

        public void Put(Action<HttpResponse> callback)
        {
            PutPost("PUT", callback);
        }

        void Invoke(string method, Action<HttpResponse> callback)
        {
            try
            {
                var request = ConfigureWebRequest(method, Url);
                GetHttpResponse(request, callback);
            }
            catch (Exception e)
            {
                OnError(e, callback);
            }
        }

        void PutPost(string method, Action<HttpResponse> callback)
        {
            try
            {
                var request = ConfigureWebRequest(method, Url);
                PreparePostBody(request);

                WriteRequestBody(request, callback);
            }
            catch (Exception e)
            {
                OnError(e, callback);
            }
        }

        #region Configure Web Request
        HttpWebRequest ConfigureWebRequest(string method, Uri url)
        {
            WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.UseDefaultCredentials = false;

            AppendHeaders(webRequest);
            AppendCookies(webRequest);

            webRequest.Method = method;

            // make sure Content-Length header is always sent since default is -1
            if (Credentials != null)
            {
                webRequest.Credentials = Credentials;
            }

            if (UserAgent.HasValue())
            {
                webRequest.UserAgent = UserAgent;
            }

            webRequest.AllowAutoRedirect = FollowRedirects;

            return webRequest;
        }

        // http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.headers.aspx
        private void AppendHeaders(HttpWebRequest webRequest)
        {
            foreach (var header in Headers)
            {
                if (_restrictedHeaderActions.ContainsKey(header.Name))
                {
                    _restrictedHeaderActions[header.Name].Invoke(webRequest, header.Value);
                }
                else
                {
                    webRequest.Headers[header.Name] = header.Value;
                }
            }
        }

        private void AppendCookies(HttpWebRequest webRequest)
        {
            webRequest.CookieContainer = new CookieContainer();
            foreach (var httpCookie in Cookies)
            {
                var cookie = new Cookie
                {
                    Name = httpCookie.Name,
                    Value = httpCookie.Value,
                    Domain = webRequest.RequestUri.Host
                };

                var uri = webRequest.RequestUri;
                webRequest.CookieContainer.Add(new Uri(string.Format("{0}://{1}", uri.Scheme, uri.Host)), cookie);
            }
        }

        #endregion

        void PreparePostBody(HttpWebRequest request)
        {
            if (HasFiles)
            {
                request.ContentType = GetMultipartFormContentType();
            }
            else if (HasParameters)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                RequestBody = EncodeParameters();
            }
            else if (HasBody)
            {
                request.ContentType = RequestContentType;
            }
        }

        string EncodeParameters()
        {
            var querystring = new StringBuilder();
            foreach (var p in Parameters)
            {
                if (querystring.Length > 1)
                    querystring.Append("&");
                querystring.AppendFormat("{0}={1}", p.Name.UrlEncode(), ((string)p.Value).UrlEncode());
            }

            return querystring.ToString();
        }

        void WriteRequestBody(HttpWebRequest request, Action<HttpResponse> callback)
        {
            if (HasBody || HasFiles)
            {
                GetRequestStream(request, callback);
            }
            else
            {
                GetHttpResponse(request, callback);
            }
        }

        void GetRequestStream(HttpWebRequest request, Action<HttpResponse> callback)
        {
            request.BeginGetRequestStream(ar =>
                {
                    using (var stream = request.EndGetRequestStream(ar))
                    {
                        if (HasFiles)
                        {
                            WriteMultipartFormData(stream);
                        }
                        else // This is when we have a RequestBody
                        {
                            stream.Write(Encoding.UTF8.GetBytes(RequestBody), 0, RequestBody.Length);
                        }
                    }

                    GetHttpResponse(request, callback);
                }, null);
        }

        #region Multipart form data

        private void WriteMultipartFormData(Stream requestStream)
        {
            var encoding = Encoding.UTF8;
            foreach (var file in Files)
            {
                // Add just the first part of this param, since we will write the file data directly to the Stream
                var header = GetMultiPartFileHeader(file);
                requestStream.Write(encoding.GetBytes(header), 0, header.Length);

                // Write the file data directly to the Stream, rather than serializing it to a string.
                file.Writer(requestStream);
                var lineEnding = Environment.NewLine;
                requestStream.Write(encoding.GetBytes(lineEnding), 0, lineEnding.Length);
            }

            foreach (var param in Parameters)
            {
                var postData = GetMultipartFormData(param);
                requestStream.Write(encoding.GetBytes(postData), 0, postData.Length);
            }

            var footer = GetMultipartFooter();
            requestStream.Write(encoding.GetBytes(footer), 0, footer.Length);
        }

        const string FormBoundary = "-----------------------------28947758029299";
        string GetMultipartFormContentType()
        {
            return string.Format("multipart/form-data; boundary={0}", FormBoundary);
        }

        string GetMultiPartFileHeader(HttpFile file)
        {
            return
                string.Format(
                    "--{0}{4}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"{4}Content-Type: {3}{4}{4}",
                    FormBoundary, file.Name, file.FileName, file.ContentType ?? "application/octet-stream",
                    Environment.NewLine);
        }

        string GetMultipartFormData(HttpParameter parameter)
        {
            return string.Format("--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}",
                                 FormBoundary, parameter.Name, parameter.Value, Environment.NewLine);
        }

        string GetMultipartFooter()
        {
            return string.Format("--{0}--{1}", FormBoundary, Environment.NewLine);
        }

        #endregion

        void GetHttpResponse(HttpWebRequest request, Action<HttpResponse> callback)
        {
            request.BeginGetResponse(ar =>
                {
                    try
                    {
                        var response = (HttpWebResponse)request.EndGetResponse(ar);
                        var result = new HttpResponse();

                        using (response)
                        {
                            result.ContentType = response.ContentType;
                            result.ContentLength = response.ContentLength;
                            result.RawBytes = response.GetResponseStream().ReadAsBytes();
                            result.Content = result.RawBytes.GetString();
                            result.StatusCode = response.StatusCode;
                            result.StatusDescription = response.StatusDescription;
                            result.ResponseUri = response.ResponseUri;
                            result.ResponseStatus = ResponseStatus.Completed;

                            if (response.Cookies != null)
                            {
                                foreach (Cookie cookie in response.Cookies)
                                {
                                    result.Cookies.Add(new HttpCookie
                                    {
                                        Comment = cookie.Comment,
                                        CommentUri = cookie.CommentUri,
                                        Discard = cookie.Discard,
                                        Domain = cookie.Domain,
                                        Expired = cookie.Expired,
                                        Expires = cookie.Expires,
                                        HttpOnly = cookie.HttpOnly,
                                        Name = cookie.Name,
                                        Path = cookie.Path,
                                        Port = cookie.Port,
                                        Secure = cookie.Secure,
                                        TimeStamp = cookie.TimeStamp,
                                        Value = cookie.Value,
                                        Version = cookie.Version
                                    });
                                }
                            }

                            foreach (var headerName in response.Headers.AllKeys)
                            {
                                var headerValue = response.Headers[headerName];
                                result.Headers.Add(new HttpHeader { Name = headerName, Value = headerValue });
                            }

                            response.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        OnError(e, callback);
                    }


                }, null);
        }

        void AddSharedHeaderActions()
        {
            _restrictedHeaderActions.Add("Accept", (r, v) => r.Accept = v);
            _restrictedHeaderActions.Add("Content-Type", (r, v) => r.ContentType = v);
            _restrictedHeaderActions.Add("Date", (r, v) =>
                                                     {
                                                         /* Set by system */
                                                     });
            _restrictedHeaderActions.Add("Host", (r, v) =>
                                                     {
                                                         /* Set by system */
                                                     });
            _restrictedHeaderActions.Add("Range", (r, v) =>
                                                      {
                                                          /* Ignore */
                                                      });
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

        void OnError(Exception e, Action<HttpResponse> callback)
        {
            var response = new HttpResponse();
            response.ErrorMessage = e.Message;
            response.ErrorException = e;
            response.ResponseStatus = ResponseStatus.Error;
            ExecuteCallback(response, callback);
        }

        void ExecuteCallback(HttpResponse response, Action<HttpResponse> callback)
        {
            var dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(() => callback(response));
        }
    }
}