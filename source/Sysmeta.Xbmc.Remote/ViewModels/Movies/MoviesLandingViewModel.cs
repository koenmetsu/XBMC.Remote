namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class MoviesLandingViewModel : Screen, IMenuItem
    {
        private readonly INavigationService navigationService;

        public const string TitleString = "movies";
        public const string DescriptionString = "hello world";

        public MoviesLandingViewModel(INavigationService navigationService, MovieTitleListViewModel titleList)
        {
            this.navigationService = navigationService;
            this.Title = TitleString;
            this.Description = DescriptionString;
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/movies.png", UriKind.RelativeOrAbsolute);

            this.Items = new[] { titleList };
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }

        public IEnumerable<IMenuItem> Items { get; set; }

        public void Selected(IMenuItem item)
        {
            if (item.Title == MovieTitleListViewModel.TitleString)
            {
                this.navigationService.UriFor<MovieTitleListViewModel>().Navigate();
            }
        }
    }
}