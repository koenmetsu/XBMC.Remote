namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System;

    using Caliburn.Micro;

    public class ServersViewModel : Screen, IMenuItem
    {
        public static string TitleString = "servers";

        public ServersViewModel()
        {
            this.Title = TitleString;
            this.Description = "the list of XBMC servers";
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/servers.png", UriKind.RelativeOrAbsolute);
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }
    }
}