namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    using Sysmeta.Xbmc.Remote.ViewModels.Movies;

    using Movie = Sysmeta.Xbmc.Remote.Model.Movie;
    using MovieFields = Sysmeta.Xbmc.Remote.Model.MovieFields;

    public interface IXbmcHost
    {
        void ListMovies(Action<IEnumerable<MovieListItemViewModel>> action);

        void GetMovieDetails(int movieId, Action<MovieListItemViewModel> action);

        void LoadImage(Uri image, Action<BitmapImage> action);
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

        public void ListMovies(Action<IEnumerable<MovieListItemViewModel>> action)
        {
            if (cache.HasValue("ListMovies"))
            {
                action(cache.Get<IEnumerable<MovieListItemViewModel>>("ListMovies"));
                return;
            }

            progressService.Show();
            this.client.VideoLibrary.GetMovies(
                (result, exception) =>
                    {
                        if (exception != null)
                        {
                            action(Enumerable.Empty<MovieListItemViewModel>());
                            progressService.Hide();
                        }
                        else
                        {
                            var movies = new List<MovieListItemViewModel>();
                            foreach (var movie in result.Movies)
                            {
                                movies.Add(new MovieListItemViewModel(this)
                                    {
                                        Id = movie.Id,
                                        Title = movie.Title,
                                        Director = movie.Director,
                                        Genre = movie.Genre,
                                        MPAA = movie.MPAA,
                                        PlayCount = movie.PlayCount,
                                        Rating = movie.Rating,
                                        Runtime = movie.Runtime,
                                        Tagline = movie.Tagline,
                                        Year = movie.Year == 0 ? "N/A" : movie.Year.ToString(),
                                        ThumbnailSource = movie.Thumbnail
                                    });
                            }

                            // Cache each movie per/id
                            foreach (var movie in movies)
                            {
                                cache.Add(movie.Id.ToString(), movie);
                            }

                            // Cache the list operation
                            cache.Add("ListMovies", movies);
                            
                            action(movies);
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

        public void GetMovieDetails(int movieId, Action<MovieListItemViewModel> action)
        {
            string key = movieId.ToString();

            if (!this.cache.HasValue(key))
            {
                action(null);
                return;
            }

            action(this.cache.Get<MovieListItemViewModel>(key));
        }

        public void LoadImage(Uri image, Action<BitmapImage> action)
        {

            this.client.Vfs.GetFile(
                image,
                (bytes, exception) =>
                    {
                        if (exception == null)
                        {
                            var img = new BitmapImage();

                            img.SetSource(new MemoryStream(bytes));

                            action(img);
                        }
                        else
                        {
                            action(null);
                        }

                    });
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