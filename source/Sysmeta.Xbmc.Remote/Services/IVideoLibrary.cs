namespace Sysmeta.Xbmc.Remote.Services
{
    using System;

    using Sysmeta.Xbmc.Remote.Model;

    public interface IVideoLibrary
    {
        void GetTvshows(Action<TvshowResult, Exception> callback);

        void GetTvEpisodes(int tvshowId, int season, Action<TvEpisodesResult, Exception> callback);

        void GetTvSeason(int tvshowId, Action<TvSeasonsResult, Exception> callback);

        void GetMovies(Action<MoviesResult, Exception> callback);

        void GetRecentlyAddedMovies(Action<MoviesResult, Exception> callback);
    }
}