using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xbmc.Http.Tests.Tests
{
    using Sysmeta.JsonRpc;

    [TestClass]
    public class HttpGet : SilverlightTest
    {
        [TestMethod]
        [Asynchronous]
        public void Get_GoogleStartPage()
        {
            var http = new HttpClient();
            http.Url = new Uri("http://www.google.com");

            bool done = false;
            http.Get(r =>
                {
                    Assert.AreEqual(218, r.ContentLength);

                    done = true;
                });

            this.EnqueueConditional(() => done);

            this.EnqueueTestComplete();
        }
    }
}
