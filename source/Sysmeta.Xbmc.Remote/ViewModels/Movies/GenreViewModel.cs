namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    using Telerik.Windows.Controls;

    public class GenreViewModel : Screen
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        public GenreViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
            this.Movies = new List<MovieViewModel>();
        }

        public string Name { get; set; }

        public ICollection<MovieViewModel> Movies { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetGenre(this.Name, model =>
                {
                    this.Movies = model.Movies;
                    NotifyOfPropertyChange(() => this.Movies);
                    NotifyOfPropertyChange(() => this.Name);
                });
        }

        public void Selected(ListBoxItemTapEventArgs eventArgs)
        {
            MovieViewModel movie = (MovieViewModel)eventArgs.Item.Content;

            this.navigationService.UriFor<MovieViewModel>()
                .WithParam(p => p.Id, movie.Id)
                .Navigate();
        }
    }
}