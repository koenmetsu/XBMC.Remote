using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xbmc.Http.Tests.Tests
{
    using System.Linq;

    using Xbmc.Client;

    [TestClass]
    public class XbmcClientTest : SilverlightTest
    {
        private XbmcClient _client;

        [TestInitialize]
        public void Init()
        {
            _client = new XbmcClient("http://localhost:8081/");
        }

        [TestMethod]
        [Tag("Mute")]
        [Asynchronous]
        public void Mute()
        {
            _client.ToggleMute();

            this.EnqueueDelay(new System.TimeSpan(0, 0, 2));
            this.EnqueueTestComplete();
        }

        [TestMethod]
        [Tag("GetMovies")]
        [Asynchronous]
        public void GetMovies()
        {
            bool done = false;

            _client.VideoLibrary.GetMovies(
                (result, exception) =>
                    {
                        var r = result;

                        var uri = r.Movies.Where(m => m.Thumbnail != null).Select(m => m.Thumbnail).First();

                        _client.Vfs.GetFile(uri, (bytes, exception1) =>
                            { 
                                var file = bytes;
                                var error = exception1;
                                done = true;
                            });
                    },
                MovieFields.Cast,
                MovieFields.Country,
                MovieFields.Director,
                MovieFields.Fanart,
                MovieFields.File,
                MovieFields.Genre,
                MovieFields.IMDBNumber,
                MovieFields.LastPlayed,
                MovieFields.MPAA,
                MovieFields.OriginalTitle,
                MovieFields.PlayCount,
                MovieFields.Plot,
                MovieFields.PlotOutline,
                MovieFields.Premiered,
                MovieFields.ProductionCode,
                MovieFields.Rating,
                MovieFields.Runtime,
                MovieFields.Set,
                MovieFields.Showlink,
                MovieFields.StreamDetails,
                MovieFields.Studio,
                MovieFields.Tagline,
                MovieFields.Thumbnail,
                MovieFields.Trailer,
                MovieFields.Votes,
                MovieFields.Writer,
                MovieFields.WritingCredits,
                MovieFields.Year);

            this.EnqueueConditional(() => done);
            this.EnqueueTestComplete();
        }
    }
}