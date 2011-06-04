namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    using Telerik.Windows.Controls;

    public class MovieGenresSelectorViewModel : Screen, IMenuItem
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        public static string TitleString = "genre";

        public MovieGenresSelectorViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
            this.Title = TitleString;
            this.Description = "bla bla bla genre is coolers";
            this.Image = null;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }

        public IObservableCollection<GenreViewModel> Genres { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetGenres(genres =>
                {
                    this.Genres = new BindableCollection<GenreViewModel>(genres);
                    NotifyOfPropertyChange(() => this.Genres);
                });
        }

        public void Selected(ListBoxItemTapEventArgs eventArgs)
        {
            var movie = (GenreViewModel)eventArgs.Item.Content;

            this.navigationService.UriFor<GenreViewModel>()
                .WithParam(p => p.Name, movie.Name)
                .Navigate();
        }
    }
}