using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xbmc.JsonRpc;

namespace Xbmc.Http.Tests.Tests
{
    [TestClass]
    public class XbmcClientTest : SilverlightTest
    {
        [TestMethod]
        [Tag("Mute")]
        [Asynchronous]
        public void Mute()
        {
            var client = new XbmcClient("http://localhost/jsonrpc");

            client.ToggleMute();

            this.EnqueueDelay(new System.TimeSpan(0, 0, 2));
            this.EnqueueTestComplete();
        }
    }
}