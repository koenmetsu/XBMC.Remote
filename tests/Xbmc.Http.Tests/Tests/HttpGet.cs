using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
            var task = http.Get();
            task.ContinueWith(task1 =>
                                  {
                                      var result = task.Result;

                                      Assert.AreEqual(218, result.ContentLength);
                                      
                                      done = true;
                                  });

            this.EnqueueConditional(() => done);

            this.EnqueueTestComplete();
        }
    }
}
