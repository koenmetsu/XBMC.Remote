namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Client;
    using Sysmeta.Xbmc.Remote.Services;

    using Telerik.Windows.Controls;

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
            this.Image = null;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }

        public IObservableCollection<Movie> Movies { get; set; }

        public void Selected(ListBoxItemTapEventArgs eventArgs)
        {
            Movie movie = (Movie)eventArgs.Item.Content;

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