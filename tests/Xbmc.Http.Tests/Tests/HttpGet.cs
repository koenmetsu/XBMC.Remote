using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xbmc.Http.Tests.Tests
{
    [TestClass]
    public class HttpGet
    {
        [TestMethod]
        public void Get_GoogleStartPage()
        {
            var http = new Http();
            http.Url = new Uri("http://www.google.com");

            var task = http.Get();

            task.Wait();

        }
    }
}
