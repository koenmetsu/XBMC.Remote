namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using Caliburn.Micro;

    using Sysmeta.Xbmc.Client;
    using Sysmeta.Xbmc.Remote.Services;

    public class MovieTitleListViewModel : Screen, IMenuItem
    {
        private readonly INavigationService navigationService;

        private readonly IXbmcHost host;

        public const string TitleString = "by title";

        public const string DescriptionString = "todo bla bla bla";

        public MovieTitleListViewModel(INavigationService navigationService, IXbmcHost host)
        {
            this.navigationService = navigationService;
            this.host = host;
            this.Title = TitleString;
            this.Description = DescriptionString;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public IObservableCollection<Movie> Movies { get; set; }

        public void Selected(Movie movie)
        {
            this.navigationService.UriFor<MovieDetailedViewModel>()
                .WithParam(p => p.MovieId, movie.MovieId)
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.ListMovies(movies =>
                {
                    this.Movies = new BindableCollection<Movie>(movies);
                    NotifyOfPropertyChange(() => Movies);
                });
        }
    }
}