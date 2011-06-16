namespace Sysmeta.Xbmc.Remote.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.ViewModels.Movies;
    using Sysmeta.Xbmc.Remote.ViewModels.Remote;
    using Sysmeta.Xbmc.Remote.ViewModels.Settings;
    using Sysmeta.Xbmc.Remote.ViewModels.Tvshows;

    public class MainMenuViewModel
    {
        private readonly INavigationService navigationService;

        private bool enabled;

        public MainMenuViewModel(INavigationService navigationService, MoviesLandingViewModel movies, TvshowsLandingViewModel tvshows, RemoteViewModel remote, SettingsViewModel settings)
        {
            this.navigationService = navigationService;
            this.MenuItems = new IMenuItem[] { movies, tvshows, settings };
        }

        public IEnumerable<IMenuItem> MenuItems { get; set; }

        public void Selected(IMenuItem item)
        {
            if (!this.enabled)
            {
                return;
            }

            if (item.Title == MoviesLandingViewModel.TitleString)
            {
                this.navigationService.UriFor<MoviesLandingViewModel>().Navigate();
            }
            else if (item.Title == TvshowsLandingViewModel.TitleString)
            {
                this.navigationService.UriFor<TvshowsLandingViewModel>().Navigate();
            }
            else if (item.Title == SettingsViewModel.TitleString)
            {
                this.navigationService.UriFor<SettingsViewModel>().Navigate();
            }
        }

        private void ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            enabled = false;
            ((StackPanel)sender).ManipulationDelta -= this.ManipulationDelta;
        }

        public void Manipulation(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            enabled = true;
            ((StackPanel)sender).ManipulationDelta += this.ManipulationDelta;
        }
    }
}