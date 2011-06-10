namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System;

    using Caliburn.Micro;

    public class AboutViewModel : Screen, IMenuItem
    {
        public static string TitleString = "about";

        public AboutViewModel()
        {
            this.Title = TitleString;
            this.Description = "about bla bla bla";
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/about.png", UriKind.RelativeOrAbsolute);
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }
    }
}