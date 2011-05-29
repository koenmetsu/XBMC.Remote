namespace Sysmeta.Xbmc.Remote.ViewModels
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.ViewModels.Movies;

    public class MainMenuViewModel
    {
        private readonly INavigationService navigationService;

        public MainMenuViewModel(INavigationService navigationService, MoviesLandingViewModel movies)
        {
            this.navigationService = navigationService;
            this.MenuItems = new[] { movies };
        }

        public IEnumerable<IMenuItem> MenuItems { get; set; }

        public void Selected(IMenuItem item)
        {
            if (item.Title == MoviesLandingViewModel.TitleString)
            {
                this.navigationService.UriFor<MoviesLandingViewModel>().Navigate();
            }
        }
    }
}