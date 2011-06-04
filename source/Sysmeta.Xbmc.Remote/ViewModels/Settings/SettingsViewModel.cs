using System;
using Caliburn.Micro;

namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    public class SettingsViewModel : Screen, IMenuItem
    {
        public SettingsViewModel()
        {
            this.Title = "settings";
            this.Description = "settings settings settings";
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/settings.png", UriKind.RelativeOrAbsolute);
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
    }
}