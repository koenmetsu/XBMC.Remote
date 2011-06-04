namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.ViewModels.Settings;

    public class MoviesLandingViewModel : Screen, IMenuItem
    {
        private readonly INavigationService navigationService;

        public const string TitleString = "movies";
        public const string DescriptionString = "hello world";

        public MoviesLandingViewModel(INavigationService navigationService, MovieTitleListViewModel titleList, MovieGenresSelectorViewModel genres)
        {
            this.navigationService = navigationService;
            this.Title = TitleString;
            this.Description = DescriptionString;
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/movies.png", UriKind.RelativeOrAbsolute);

            this.Items = new IMenuItem[] { titleList, genres };
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
            else if(item.Title == MovieGenresSelectorViewModel.TitleString)
            {
                this.navigationService.UriFor<MovieGenresSelectorViewModel>().Navigate();
            }
        }
    }
}