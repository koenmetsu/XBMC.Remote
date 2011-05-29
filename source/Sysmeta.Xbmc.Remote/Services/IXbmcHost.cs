namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sysmeta.Xbmc.Client;

    public interface IXbmcHost
    {
        void ListMovies(Action<IEnumerable<Movie>> action);

        void GetMovieDetails(int movieId, Action<Movie> action);
    }

    public class XbmcHost : IXbmcHost
    {
        private readonly ICache cache;

        private readonly IProgressService progressService;

        private XbmcClient client;

        public XbmcHost(ICache cache, IProgressService progressService)
        {
            this.cache = cache;
            this.progressService = progressService;
            this.client = new XbmcClient("http://FENPC100:8081/");
        }

        public void ListMovies(Action<IEnumerable<Movie>> action)
        {
            if (cache.HasValue("ListMovies"))
            {
                action(cache.Get<IEnumerable<Movie>>("ListMovies"));
                return;
            }

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
                            foreach (var movie in result.Movies)
                            {
                                cache.Add(movie.MovieId.ToString(), movie);
                            }

                            cache.Add("ListMovies", result.Movies);
                            action(result.Movies);
                            progressService.Hide();
                        }
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
                MovieFields.Title,
                MovieFields.Top250,
                MovieFields.Trailer,
                MovieFields.Votes,
                MovieFields.Writer,
                MovieFields.WritingCredits,
                MovieFields.Year);
        }

        public void GetMovieDetails(int movieId, Action<Movie> action)
        {
            string key = movieId.ToString();

            if (!this.cache.HasValue(key))
            {
                action(null);
                return;
            }

            action(this.cache.Get<Movie>(key));
        }
    }

    public interface ICache
    {
        void Add(string key, object value);
        T Get<T>(string key);
        bool HasValue(string key);
    }

    public class Cache : ICache
    {
        private Dictionary<string, object> cache = new Dictionary<string, object>(); 

        public void Add(string key, object value)
        {
            cache[key] = value;
        }

        public T Get<T>(string key)
        {
            return (T)this.cache[key];
        }

        public bool HasValue(string key)
        {
            return this.cache.ContainsKey(key);
        }
    }
}