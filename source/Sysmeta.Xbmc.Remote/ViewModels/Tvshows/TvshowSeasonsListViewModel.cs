namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Telerik.Windows.Controls;

    public class TvshowSeasonsListViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private IEnumerable<TvSeasonViewModel> seasons;

        public TvshowSeasonsListViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public IEnumerable<TvSeasonViewModel> Seasons
        {
            get
            {
                return this.seasons;
            }
            
            set
            {
                this.seasons = value;
                NotifyOfPropertyChange(() => this.Seasons);
            }
        }

        public void Selected(ListBoxItemTapEventArgs eventArgs)
        {
            TvSeasonViewModel movie = (TvSeasonViewModel)eventArgs.Item.Content;

            this.navigationService.UriFor<TvEpisodeListViewModel>()
                .WithParam(p => p.Season, movie.Season)
                .WithParam(p => p.TvshowId, movie.TvshowId)
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}