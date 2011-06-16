using System;
using Caliburn.Micro;

namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System.Collections.Generic;

    public class SettingsViewModel : Screen, IMenuItem
    {
        private readonly INavigationService navigationService;

        public static string TitleString = "settings";

        public SettingsViewModel(INavigationService navigationService, ConnectionsViewModel connections, AboutViewModel about)
        {
            this.navigationService = navigationService;
            this.Title = TitleString;
            this.Description = "application settings";
            this.Image = "/Sysmeta.Xbmc.Remote;component/Images/{0}/settings.png";

            this.MenuItems = new IMenuItem[] { connections, about };
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IEnumerable<IMenuItem> MenuItems { get; set; }

        public void Selected(IMenuItem item)
        {
            if (item.Title == AboutViewModel.TitleString)
            {
                this.navigationService.UriFor<AboutViewModel>().Navigate();
            }
            else if (item.Title == ConnectionsViewModel.TitleString)
            {
                this.navigationService.UriFor<ConnectionsViewModel>().Navigate();
            }
        }
    }
}