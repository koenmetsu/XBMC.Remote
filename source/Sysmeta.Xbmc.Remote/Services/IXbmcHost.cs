namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.ViewModels.Movies;

    using Movie = Sysmeta.Xbmc.Remote.Model.Movie;
    using MovieFields = Sysmeta.Xbmc.Remote.Model.MovieFields;

    public interface IXbmcHost
    {
        void ListMovies(Action<IEnumerable<MovieViewModel>> action);

        void GetMovieDetails(int movieId, Action<MovieViewModel> action);

        void LoadImage(Uri image, Action<BitmapImage> action);

        void GetGenres(Action<IEnumerable<GenreViewModel>> action);

        void GetGenre(string genre, Action<GenreViewModel> action);
    }

    public class XbmcHost : IXbmcHost
    {
        private readonly ICache cache;

        private readonly IProgressService progressService;

        private readonly INavigationService navigationService;

        private XbmcClient client;

        public XbmcHost(ICache cache, IProgressService progressService, INavigationService navigationService)
        {
            this.cache = cache;
            this.progressService = progressService;
            this.navigationService = navigationService;
            this.client = new XbmcClient("http://FENPC100:8081/");
        }

        public void ListMovies(Action<IEnumerable<MovieViewModel>> action)
        {
            if (cache.HasValue("ListMovies"))
            {
                action(cache.Get<IEnumerable<MovieViewModel>>("ListMovies"));
                return;
            }

            progressService.Show();
            this.client.VideoLibrary.GetMovies(
                (result, exception) =>
                    {
                        if (exception != null)
                        {
                            action(Enumerable.Empty<MovieViewModel>());
                            progressService.Hide();
                        }
                        else
                        {
                            var movies = new List<MovieViewModel>();
                            foreach (var movie in result.Movies)
                            {
                                movies.Add(new MovieViewModel(this)
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

        public void GetMovieDetails(int movieId, Action<MovieViewModel> action)
        {
            string key = movieId.ToString();

            if (!this.cache.HasValue(key))
            {
                action(null);
                return;
            }

            action(this.cache.Get<MovieViewModel>(key));
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

        public void GetGenre(string genre, Action<GenreViewModel> action)
        {
            if (this.cache.HasValue(genre))
            {
                action(this.cache.Get<GenreViewModel>(genre));
            }
            else
            {
                this.GetGenres(genres =>
                    {
                        action(genres.Where(g => g.Name == genre).FirstOrDefault());
                    });
            }
        }

        public void GetGenres(Action<IEnumerable<GenreViewModel>> action)
        {
            if (this.cache.HasValue("GetGenres"))
            {
                action(this.cache.Get<IEnumerable<GenreViewModel>>("GetGenres"));
                return;
            }

            this.ListMovies(movies =>
                {
                    var lookup = new Dictionary<string, GenreViewModel>();

                    foreach (var movie in movies)
                    {
                        foreach (var genre in SplitGenre(movie.Genre))
                        {
                            GenreViewModel vm;
                            if (!lookup.TryGetValue(genre, out vm))
                            {
                                vm = new GenreViewModel(this, this.navigationService) { Name = genre };
                                lookup.Add(genre, vm);
                            }

                            vm.Movies.Add(movie);
                        }
                    }

                    foreach (var genre in lookup.Values)
                    {
                        this.cache.Add(genre.Name, genre);
                    }

                    this.cache.Add("GetGenres", lookup.Values);

                    action(lookup.Values);
                });
        }

        private static IEnumerable<string> SplitGenre(string genres)
        {
            if (string.IsNullOrEmpty(genres))
            {
                yield break;
            }

            foreach (var genre in genres.Split('/'))
            {
                yield return genre.Trim();
            }
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