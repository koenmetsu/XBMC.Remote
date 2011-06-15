namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public string Genre { get; set; }

        public string Premiered { get; set; }

        public float Rating { get; set; }

        public string Studio { get; set; }

        public int Year { get; set; }

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
                    this.Genre = show.Genre;
                    this.Premiered = show.Premiered.ToShortDateString();
                    this.Rating = show.Rating;
                    this.Studio = show.Studio;
                    this.Year = show.Year;
                    NotifyOfPropertyChange(() => this.Plot);
                    NotifyOfPropertyChange(() => this.Title);
                    NotifyOfPropertyChange(() => this.Genre);
                    NotifyOfPropertyChange(() => this.Premiered);
                    NotifyOfPropertyChange(() => this.Rating);
                    NotifyOfPropertyChange(() => this.Studio);
                    NotifyOfPropertyChange(() => this.Year);

                    this.host.GetTvSeasons(this.TvShowId, seasons =>
                    {
                        this.SeasonList = new TvshowSeasonsListViewModel(navigationService) { Seasons = seasons.Select(s => new TvSeasonViewModel(host, navigationService, s)) };

                        NotifyOfPropertyChange(() => this.SeasonList);
                    });
                });
        }
    }
}