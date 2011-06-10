namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.ViewModels.Movies;

    using Sysmeta.Xbmc.Remote.ViewModels.Tvshows;

    using Action = Caliburn.Micro.Action;
    using Movie = Sysmeta.Xbmc.Remote.Model.Movie;
    using MovieFields = Sysmeta.Xbmc.Remote.Model.MovieFields;

    public interface IXbmcHost
    {
        void ListMovies(Action<IEnumerable<MovieViewModel>> action);

        void GetMovieDetails(int movieId, Action<MovieViewModel> action);

        void LoadImage(Uri image, Action<BitmapImage> action);

        void GetGenres(Action<IEnumerable<GenreViewModel>> action);

        void GetGenre(string genre, Action<GenreViewModel> action);

        void PlayMovie(int movieId);

        void PlayEpisode(int episodeId);

        void GetTvshows(Action<IEnumerable<TvshowViewModel>> action);

        void GetTvshow(int tvshowId, Action<TvshowViewModel> action);

        void GetTvSeasons(int tvshowId, Action<IEnumerable<TvSeasonViewModel>> action);

        void GetTvEpisodes(int tvshowId, int season, Action<IEnumerable<TvEpisodeViewModel>> action);
    }

    public class XbmcHost : IXbmcHost
    {
        private readonly ICache cache;

        private readonly IProgressService progressService;

        private readonly INavigationService navigationService;

        private readonly SettingsHost settingsHost;

        private XbmcClient client;

        public XbmcHost(ICache cache, IProgressService progressService, INavigationService navigationService, SettingsHost settingsHost)
        {
            this.cache = cache;
            this.progressService = progressService;
            this.navigationService = navigationService;
            this.settingsHost = settingsHost;
            this.client = new XbmcClient(this.settingsHost.Settings.Active.Url.ToString());
        }

        public void PlayEpisode(int episodeId)
        {
            this.client.PlayEpisode(episodeId);
        }

        public void GetTvshows(Action<IEnumerable<TvshowViewModel>> action)
        {
            if (this.cache.HasValue("GetTvshows"))
            {
                action(this.cache.Get<IEnumerable<TvshowViewModel>>("GetTvshows"));
                return;
            }

            progressService.Show();
            this.client.Video.GetTvshows((shows, e) =>
                                         {
                                             if (e != null)
                                             {
                                                 action(Enumerable.Empty<TvshowViewModel>());
                                                 progressService.Hide();
                                             }
                                             else
                                             {
                                                 var tvshows = new List<TvshowViewModel>();
                                                 foreach (var tvshow in shows.Tvshows)
                                                 {
                                                     tvshows.Add(new TvshowViewModel(this)
                                                                 {
                                                                     Id = tvshow.Id,
                                                                     Title = tvshow.Title,
                                                                     Episodes = tvshow.Episode,
                                                                     FanartSource = tvshow.Fanart,
                                                                     Genre = tvshow.Genre,
                                                                     PlayCount = tvshow.PlayCount,
                                                                     Plot = tvshow.Plot,
                                                                     Premiered = tvshow.Premiered.ToShortDateString(),
                                                                     Rating = tvshow.Rating,
                                                                     Studio = tvshow.Studio,
                                                                     ThumbnailSource = tvshow.Thumbnail, 
                                                                 });
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

        public void GetTvshow(int tvshowId, Action<TvshowViewModel> action)
        {
            if (this.cache.HasValue(this.GetTvshowCacheKey(tvshowId)))
            {
                action(this.cache.Get<TvshowViewModel>(this.GetTvshowCacheKey(tvshowId)));
            }
            else
            {
                this.GetTvshows(shows => action(shows.Where(s => s.Id == tvshowId).FirstOrDefault()));
            }
        }

        public void GetTvEpisodes(int tvshowId, int season, Action<IEnumerable<TvEpisodeViewModel>> action)
        {
            string cacheKey = this.GetEpisodesCacheKey(tvshowId, season);
            if (this.cache.HasValue(cacheKey))
            {
                action(this.cache.Get<IEnumerable<TvEpisodeViewModel>>(cacheKey));
                return;
            }

            progressService.Show();
            this.client.Video.GetTvEpisodes(tvshowId, season, (result, exception) =>
                {
                    if (exception != null)
                    {
                        action(Enumerable.Empty<TvEpisodeViewModel>());
                        progressService.Hide();
                    }
                    else
                    {
                        var episodes = new List<TvEpisodeViewModel>();
                        foreach (var episode in result.Episodes)
                        {
                            var viewModel = new TvEpisodeViewModel(this)
                                {
                                    Id = episode.Id,
                                    Title = episode.Title,
                                    Year = episode.Year,
                                    Rating = episode.Rating,
                                    Director = episode.Director,
                                    Plot = episode.Plot,
                                    LastPlayed = episode.LastPlayed,
                                    ShowTitle = episode.ShowTitle,
                                    FirstAired = episode.FirstAired,
                                    Duration = episode.Duration,
                                    Season = episode.Season,
                                    Episode = episode.Episode,
                                    PlayCount = episode.PlayCount,
                                    Writer = episode.Writer,
                                    Studio = episode.Studio,
                                    MPAA = episode.MPAA,
                                    Premiered = episode.Premiered,
                                    FanartSource = episode.Fanart
                                };
                            episodes.Add(viewModel);
                        }

                        this.cache.Add(cacheKey, episodes);

                        action(episodes);
                        progressService.Hide();
                    }
                });
        }

        public void GetTvSeasons(int tvshowId, Action<IEnumerable<TvSeasonViewModel>> action)
        {
            string cacheKey = this.GetTvSeasonsCacheKey(tvshowId);

            if (this.cache.HasValue(cacheKey))
            {
                action(this.cache.Get<IEnumerable<TvSeasonViewModel>>(cacheKey));
                return;
            }

            progressService.Show();
            this.client.Video.GetTvSeason(tvshowId, (result, exception) =>
                {
                    if (exception != null || result.Seasons == null)
                    {
                        action(Enumerable.Empty<TvSeasonViewModel>());
                        progressService.Hide();
                    }
                    else
                    {
                        var seasons = new List<TvSeasonViewModel>();
                        foreach (var season in result.Seasons)
                        {
                            var vm = new TvSeasonViewModel(this)
                                {
                                    TvshowId = tvshowId,
                                    Season = season.Season,
                                    Title = season.Title,
                                    ThumbnailSource = season.Thumbnail,
                                    Episodes = season.Episodes
                                };
                            seasons.Add(vm);
                        }

                        this.cache.Add(cacheKey, seasons);

                        action(seasons);
                        progressService.Hide();
                    }
                });
        }

        public void ListMovies(Action<IEnumerable<MovieViewModel>> action)
        {
            if (cache.HasValue("ListMovies"))
            {
                action(cache.Get<IEnumerable<MovieViewModel>>("ListMovies"));
                return;
            }

            progressService.Show();
            this.client.Video.GetMovies(
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
                                cache.Add(this.GetMovieCacheKey(movie.Id), movie);
                            }

                            // Cache the list operation
                            cache.Add("ListMovies", movies);
                            
                            action(movies);
                            progressService.Hide();
                        }
                    });
        }

        public void GetMovieDetails(int movieId, Action<MovieViewModel> action)
        {
            string key = this.GetMovieCacheKey(movieId);

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

        public void PlayMovie(int movieId)
        {
            this.client.PlayMovie(movieId);
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