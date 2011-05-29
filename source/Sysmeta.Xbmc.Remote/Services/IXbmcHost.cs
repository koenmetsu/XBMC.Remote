namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sysmeta.Xbmc.Client;

    public interface IXbmcHost
    {
        void ListMovies(Action<IEnumerable<Movie>> action);

        void GetMovieDetail(int movieId, Action<Movie> action);
    }

    public class XbmcHost : IXbmcHost
    {
        private readonly IProgressService progressService;

        private XbmcClient client;

        public XbmcHost(IProgressService progressService)
        {
            this.progressService = progressService;
            this.client = new XbmcClient("http://FENPC100:8081/");
        }

        public void ListMovies(Action<IEnumerable<Movie>> action)
        {
            progressService.Show();
            this.client.VideoLibrary.GetMovies(
                (result, exception) =>
                    {
                        if (exception != null)
                        {
                            action(Enumerable.Empty<Movie>());
                            progressService.Hide();
                        }
                        else
                        {
                            action(result.Movies);
                            progressService.Hide();
                        }
                    },
                MovieFields.Title,
                MovieFields.Director,
                MovieFields.Genre,
                MovieFields.Rating,
                MovieFields.Year,
                MovieFields.Thumbnail,
                MovieFields.Runtime);
        }

        public void GetMovieDetail(int movieId, Action<Movie> action)
        {
            progressService.Show();

            this.client.VideoLibrary.GetMovieDetails(
                (movie, exception) =>
                    {
                        action(movie);
                        progressService.Hide();
                    },
                movieId,
                MovieFields.Title,
                MovieFields.Director,
                MovieFields.Genre,
                MovieFields.Rating,
                MovieFields.Year,
                MovieFields.Thumbnail,
                MovieFields.Runtime
                /*MovieFields.Cast,
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
                MovieFields.Title,
                MovieFields.Top250,
                MovieFields.Trailer,
                MovieFields.Votes,
                MovieFields.Writer,
                MovieFields.WritingCredits,
                MovieFields.Year*/);
        }
    }
}