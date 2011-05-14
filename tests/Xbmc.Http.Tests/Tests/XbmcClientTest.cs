using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xbmc.Http.Tests.Tests
{
    using Xbmc.Client;

    [TestClass]
    public class XbmcClientTest : SilverlightTest
    {
        private XbmcClient _client;

        [TestInitialize]
        public void Init()
        {
            _client = new XbmcClient("http://localhost/jsonrpc");
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
            this.EnqueueCallback(() => _client.VideoLibrary.GetMovies(MovieFields.Cast, MovieFields.Country, MovieFields.Director, MovieFields.Fanart, MovieFields.File, MovieFields.Genre, MovieFields.IMDBNumber, MovieFields.LastPlayed, MovieFields.MPAA, MovieFields.OriginalTitle, MovieFields.PlayCount, MovieFields.Plot, MovieFields.PlotOutline, MovieFields.Premiered, MovieFields.ProductionCode, MovieFields.Rating, MovieFields.Runtime, MovieFields.Set, MovieFields.Showlink, MovieFields.StreamDetails, MovieFields.Studio, MovieFields.Tagline, MovieFields.Thumbnail, MovieFields.Trailer, MovieFields.Votes, MovieFields.Writer, MovieFields.WritingCredits, MovieFields.Year));

            this.EnqueueDelay(new System.TimeSpan(0, 0, 2));
            this.EnqueueTestComplete();
        }
    }
}