namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class TvshowSeasonsViewModel : Screen
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        public TvshowSeasonsViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
        }

        public int TvShowId { get; set; }

        public string Title { get; set; }

        public string Plot { get; set; }

        public TvshowSeasonsListViewModel SeasonList { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetTvshow(this.TvShowId, show =>
                {
                    if (show == null)
                    {
                        navigationService.GoBack();
                        return;
                    }

                    this.Title = show.Title;
                    this.Plot = show.Plot;
                });
            this.host.GetTvSeasons(this.TvShowId, seasons =>
                {
                    this.SeasonList = new TvshowSeasonsListViewModel(navigationService) { Seasons = seasons };

                    NotifyOfPropertyChange(() => this.SeasonList);
                    NotifyOfPropertyChange(() => this.Title);
                });
        }
    }
}