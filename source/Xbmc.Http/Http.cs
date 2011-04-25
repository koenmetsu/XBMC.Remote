﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xbmc.Http.Extensions;

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

        async Task<HttpResponse> WriteRequestBody(HttpWebRequest request)
        {
            if (HasBody || HasFiles)
            {
                return await GetRequestStream(request);
            }

            return await GetHttpResponse(request);
        }

        async Task<HttpResponse> GetRequestStream(HttpWebRequest request)
        {
            using (var stream = await request.GetRequestStreamAsync())
            {
                if (HasFiles)
                {
                    await WriteMultipartFormData(stream);
                }
                else // This is when we have a RequestBody
                {
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(RequestBody), 0, RequestBody.Length);
                }
            }

            return await GetHttpResponse(request);
        }

        #region Multipart form data

        async Task WriteMultipartFormData(Stream stream)
        {
            foreach (var file in Files)
            {
                string header = GetMultiPartFileHeader(file);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(header), 0, header.Length);

                await file.Writer(stream);

                string eol = Environment.NewLine;
                await stream.WriteAsync(Encoding.UTF8.GetBytes(eol), 0, eol.Length);
            }

            foreach (var parameter in Parameters)
            {
                string data = GetMultipartFormData(parameter);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(data), 0, data.Length);
            }

            string footer = GetMultipartFooter();
            await stream.WriteAsync(Encoding.UTF8.GetBytes(footer), 0, footer.Length);
        }

        private const string FormBoundary = "-----------------------------28947758029299";

        private string GetMultiPartFileHeader(HttpFile file)
        {
            return
                string.Format(
                    "--{0}{4}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"{4}Content-Type: {3}{4}{4}",
                    FormBoundary, file.Name, file.FileName, file.ContentType ?? "application/octet-stream",
                    Environment.NewLine);
        }

        private string GetMultipartFormData(HttpParameter parameter)
        {
            return string.Format("--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}",
                                 FormBoundary, parameter.Name, parameter.Value, Environment.NewLine);
        }

        private string GetMultipartFooter()
        {
            return string.Format("--{0}--{1}", FormBoundary, Environment.NewLine);
        }

        #endregion

        async Task<HttpResponse> GetHttpResponse(HttpWebRequest request)
        {
            var response = (HttpWebResponse)await request.GetResponseAsync();
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

            return result;
        }

        private void AddSharedHeaderActions()
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

        private void AddAsyncHeaderActions()
        {
            // WP7 doesn't as of Beta doesn't support a way to set Content-Length either directly
            // or indirectly
            _restrictedHeaderActions.Add("Content-Length", (r, v) => { });
        }

        private void AddSyncHeaderActions()
        {
            _restrictedHeaderActions.Add("User-Agent", (r, v) => r.UserAgent = v);
        }
    }
}