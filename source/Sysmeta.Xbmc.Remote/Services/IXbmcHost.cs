namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Model;
    using Sysmeta.Xbmc.Remote.ViewModels.Movies;

    using Sysmeta.Xbmc.Remote.ViewModels.Tvshows;

    using Action = Caliburn.Micro.Action;
    using Movie = Sysmeta.Xbmc.Remote.Model.Movie;
    using MovieFields = Sysmeta.Xbmc.Remote.Model.MovieFields;

    public interface IXbmcHost
    {
        void ListMovies(Action<IEnumerable<Movie>> action);

        void GetMovieDetails(int movieId, Action<Movie> action);

        void LoadImage(Uri image, Action<BitmapImage> action);

        void GetGenres(Action<IEnumerable<GenreViewModel>> action);

        void GetGenre(string genre, Action<GenreViewModel> action);

        void PlayMovie(int movieId);

        void PlayEpisode(int episodeId);

        void GetTvshows(Action<IEnumerable<Tvshow>> action);

        void GetTvshow(int tvshowId, Action<Tvshow> action);

        void GetTvSeasons(int tvshowId, Action<IEnumerable<TvSeason>> action);

        void GetTvEpisodes(int tvshowId, int season, Action<IEnumerable<TvEpisode>> action);

        void GetRecentlyAddedMovies(Action<IEnumerable<Movie>> action);
    }

    public class XbmcHost : IXbmcHost
    {
        private readonly ICache cache;

        private readonly IProgressService progressService;

        private readonly INavigationService navigationService;

        private readonly SettingsHost settingsHost;

        private IXbmcClient client;

        private Uri currentUri;

        private string currentUsername;

        private string currentPassword;

        public XbmcHost(ICache cache, IProgressService progressService, INavigationService navigationService, SettingsHost settingsHost)
        {
            this.cache = cache;
            this.progressService = progressService;
            this.navigationService = navigationService;
            this.settingsHost = settingsHost;
        }

        public void PlayEpisode(int episodeId)
        {
            this.SetClient();
            this.client.PlayEpisode(episodeId);
        }

        public void GetTvshows(Action<IEnumerable<Tvshow>> action)
        {
            if (!this.SetClient())
            {
                action(null);
                return;
            }

            if (this.cache.HasValue("GetTvshows"))
            {
                action(this.cache.Get<IEnumerable<Tvshow>>("GetTvshows"));
                return;
            }

            progressService.Show();
            this.client.Video.GetTvshows((shows, e) =>
                                         {
                                             if (e != null)
                                             {
                                                 action(Enumerable.Empty<Tvshow>());
                                                 progressService.Hide();
                                             }
                                             else
                                             {
                                                 var tvshows = new List<Tvshow>();
                                                 foreach (var tvshow in shows.Tvshows)
                                                 {
                                                     tvshows.Add(tvshow);
                                                 }

                                                 this.cache.Add("GetTvshows", tvshows);
                                                 foreach (var tvshow in tvshows)
                                                 {
                                                     this.cache.Add(this.GetTvshowCacheKey(tvshow.Id), tvshow);
                                                 }

                                                 action(tvshows);
                                                 progressService.Hide();
                                             }
                                         });
        }

        public void GetTvshow(int tvshowId, Action<Tvshow> action)
        {
            this.SetClient();

            if (this.cache.HasValue(this.GetTvshowCacheKey(tvshowId)))
            {
                action(this.cache.Get<Tvshow>(this.GetTvshowCacheKey(tvshowId)));
            }
            else
            {
                this.GetTvshows(shows => action(shows.Where(s => s.Id == tvshowId).FirstOrDefault()));
            }
        }

        public void GetTvEpisodes(int tvshowId, int season, Action<IEnumerable<TvEpisode>> action)
        {
            this.SetClient();

            string cacheKey = this.GetEpisodesCacheKey(tvshowId, season);
            if (this.cache.HasValue(cacheKey))
            {
                action(this.cache.Get<IEnumerable<TvEpisode>>(cacheKey));
                return;
            }

            progressService.Show();
            this.client.Video.GetTvEpisodes(tvshowId, season, (result, exception) =>
                {
                    if (exception != null)
                    {
                        action(Enumerable.Empty<TvEpisode>());
                        progressService.Hide();
                    }
                    else
                    {
                        var episodes = new List<TvEpisode>();
                        foreach (var episode in result.Episodes)
                        {
                            episodes.Add(episode);
                        }

                        this.cache.Add(cacheKey, episodes);

                        action(episodes);
                        progressService.Hide();
                    }
                });
        }

        public void GetRecentlyAddedMovies(Action<IEnumerable<Movie>> action)
        {
            if (!this.SetClient())
            {
                action(null);
                return;
            }

            if (this.cache.HasValue("GetRecentlyAddedMovies"))
            {
                action(this.cache.Get<IEnumerable<Movie>>("GetRecentlyAddedMovies"));
                return;
            }

            this.client.Video.GetRecentlyAddedMovies((result, exception) =>
                {
                    if (exception != null)
                    {
                        action(Enumerable.Empty<Movie>());
                        progressService.Hide();
                    }
                    else
                    {
                        this.cache.Add("GetRecentlyAddedMovies", result.Movies);

                        action(result.Movies);
                        progressService.Hide();
                    }
                });
        }

        public void GetTvSeasons(int tvshowId, Action<IEnumerable<TvSeason>> action)
        {
            this.SetClient();

            string cacheKey = this.GetTvSeasonsCacheKey(tvshowId);

            if (this.cache.HasValue(cacheKey))
            {
                action(this.cache.Get<IEnumerable<TvSeason>>(cacheKey));
                return;
            }

            progressService.Show();
            this.client.Video.GetTvSeason(tvshowId, (result, exception) =>
                {
                    if (exception != null || result.Seasons == null)
                    {
                        action(Enumerable.Empty<TvSeason>());
                        progressService.Hide();
                    }
                    else
                    {
                        var seasons = new List<TvSeason>();
                        foreach (var season in result.Seasons)
                        {
                            season.TvshowId = tvshowId;
                            seasons.Add(season);
                        }

                        this.cache.Add(cacheKey, seasons);

                        action(seasons);
                        progressService.Hide();
                    }
                });
        }

        public void ListMovies(Action<IEnumerable<Movie>> action)
        {
            if (!this.SetClient())
            {
                action(null);
                return;
            }

            if (cache.HasValue("ListMovies"))
            {
                action(cache.Get<IEnumerable<Movie>>("ListMovies"));
                return;
            }

            progressService.Show();
            this.client.Video.GetMovies(
                (result, exception) =>
                    {
                        if (exception != null)
                        {
                            action(Enumerable.Empty<Movie>());
                            progressService.Hide();
                        }
                        else
                        {
                            var movies = new List<Movie>();
                            foreach (var movie in result.Movies)
                            {
                                movies.Add(movie);
                            }

                            // Cache each movie per/id
                            foreach (var movie in movies)
                            {
                                cache.Add(this.GetMovieCacheKey(movie.Id), movie);
                            }

                            // Cache the list operation
                            cache.Add("ListMovies", movies);
                            
                            action(movies);
                            progressService.Hide();
                        }
                    });
        }

        public void GetMovieDetails(int movieId, Action<Movie> action)
        {
            this.SetClient();

            string key = this.GetMovieCacheKey(movieId);

            if (!this.cache.HasValue(key))
            {
                this.ListMovies(movies => action(movies.Where(m => m.Id == movieId).FirstOrDefault()));
                return;
            }

            action(this.cache.Get<Movie>(key));
        }

        public void LoadImage(Uri image, Action<BitmapImage> action)
        {
            this.SetClient();

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
            if (!this.SetClient())
            {
                action(null);
                return;
            }

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

        public void PlayMovie(int movieId)
        {
            this.SetClient();

            this.client.PlayMovie(movieId);
        }

        public void GetGenres(Action<IEnumerable<GenreViewModel>> action)
        {
            if (!this.SetClient())
            {
                action(null);
                return;
            }

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

                            vm.Movies.Add(new MovieViewModel(this, navigationService, movie));
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

        private string GetTvshowCacheKey(int tvshowId)
        {
            return string.Format("tvshowid{0}", tvshowId);
        }

        private string GetTvSeasonsCacheKey(int tvshowId)
        {
            return string.Format("GetTvSeasons{0}", tvshowId);
        }

        private string GetMovieCacheKey(int movieId)
        {
            return string.Format("movieid{0}", movieId);
        }

        private string GetEpisodesCacheKey(int tvshowId, int season)
        {
            return string.Format("episodes{0}{1}", tvshowId, season);
        }

        private bool SetClient()
        {
            var active = this.settingsHost.Settings.Active;

            if (active != null && this.currentUri == active.Url && this.currentUsername == active.Username && this.currentPassword == active.Password)
            {
                return true;
            }

            if (active != null && (this.currentUri != active.Url || this.currentUsername != active.Username || this.currentPassword != active.Password))
            {
                if (active.Url.Host == "demo" && active.Url.Port == 1234)
                {
                    this.currentUri = active.Url;
                    this.client = new DemoXbmcClient();
                    this.cache.Clear();
                }
                else
                {
                    this.currentUri = active.Url;
                    this.currentUsername = active.Username;
                    this.currentPassword = active.Password;
                    this.client = new XbmcClient(active.Url.ToString(), active.Username, active.Password);
                    this.cache.Clear();
                }

                return true;
            }

            return false;
        }
    }
}