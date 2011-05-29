namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class MovieDetailedViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly IXbmcHost host;

        public MovieDetailedViewModel(INavigationService navigationService, IXbmcHost host)
        {
            this.navigationService = navigationService;
            this.host = host;
        }

        public string Title { get; set; }

        public int MovieId { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetMovieDetails(this.MovieId, movie =>
                {
                    if (movie == null)
                    {
                        this.navigationService.GoBack();
                        return;
                    }

                    this.Title = movie.Title;
                    NotifyOfPropertyChange(() => this.Title);
                });
        }
    }
}