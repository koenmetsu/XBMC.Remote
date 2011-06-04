namespace Sysmeta.Xbmc.Remote.ViewModels
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.ViewModels.Movies;
    using Sysmeta.Xbmc.Remote.ViewModels.Remote;
    using Sysmeta.Xbmc.Remote.ViewModels.Settings;
    using Sysmeta.Xbmc.Remote.ViewModels.Tvshows;

    public class MainMenuViewModel
    {
        private readonly INavigationService navigationService;

        public MainMenuViewModel(INavigationService navigationService, MoviesLandingViewModel movies, TvshowsLandingViewModel tvshows, RemoteViewModel remote, SettingsViewModel settings)
        {
            this.navigationService = navigationService;
            this.MenuItems = new IMenuItem[] { movies, tvshows, remote, settings };
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