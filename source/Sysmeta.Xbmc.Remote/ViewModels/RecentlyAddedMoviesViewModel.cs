namespace Sysmeta.Xbmc.Remote.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;
    using Sysmeta.Xbmc.Remote.ViewModels.Movies;

    public class RecentlyAddedMoviesViewModel : PropertyChangedBase
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        private bool enabled;

        public RecentlyAddedMoviesViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;

            host.GetRecentlyAddedMovies(movies =>
                {
                    this.Movies = movies.Select(m => new MovieViewModel(host, navigationService, m)).ToList();
                    NotifyOfPropertyChange(() => this.Movies);
                });
        }

        public IEnumerable<MovieViewModel> Movies { get; set; }

        public void Selected(MovieViewModel item)
        {
            if (!this.enabled)
            {
                return;
            }

            this.navigationService.UriFor<MovieViewModel>()
                .WithParam(m => m.Id, item.Id)
                .Navigate();
        }

        private void ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            enabled = false;
            ((Grid)sender).ManipulationDelta -= this.ManipulationDelta;
        }

        public void Manipulation(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            enabled = true;
            ((Grid)sender).ManipulationDelta += this.ManipulationDelta;
        }
    }
}