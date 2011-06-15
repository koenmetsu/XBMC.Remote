namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    using Telerik.Windows.Controls;

    public class TvEpisodeListViewModel : Screen
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        public TvEpisodeListViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
        }

        public string SeasonTitle { get; set; }

        public string TvshowName { get; set; }

        public int Season { get; set; }

        public int TvshowId { get; set; }

        public IEnumerable<TvEpisodeViewModel> Episodes { get; set; }

        public void Selected(ListBoxItemTapEventArgs eventArgs)
        {
            TvEpisodeViewModel movie = (TvEpisodeViewModel)eventArgs.Item.Content;

            this.navigationService.UriFor<TvEpisodeViewModel>()
                .WithParam(p => p.Season, movie.Season)
                .WithParam(p => p.Id, movie.Id)
                .WithParam(p => p.TvshowId, this.TvshowId )
                .WithParam(p => p.Episode, movie.Episode)
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetTvEpisodes(this.TvshowId, this.Season, episodes =>
                {
                    this.Episodes = episodes.OrderBy(e => e.Episode).Select(e => new TvEpisodeViewModel(host, navigationService, e)).ToList();

                    this.SeasonTitle = string.Format("Season {0}", this.Season);
                    this.TvshowName = this.Episodes.First().ShowTitle;

                    NotifyOfPropertyChange(() => this.Episodes);
                    NotifyOfPropertyChange(() => this.SeasonTitle);
                    NotifyOfPropertyChange(() => this.TvshowName);
                });
        }
    }
}