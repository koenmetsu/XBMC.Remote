namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System;

    using Caliburn.Micro;

    using Microsoft.Phone.Tasks;

    public class AboutViewModel : Screen, IMenuItem
    {
        public static string TitleString = "about";

        public AboutViewModel()
        {
            this.Title = TitleString;
            this.Description = "about this application";
            this.Image = "/Sysmeta.Xbmc.Remote;component/Images/{0}/about.png";
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public void OpenProjectHomepage()
        {
            var task = new WebBrowserTask() { URL = "http://github.com/fen/Xbmc.Remote/" };
            task.Show();
        }

        public void OpenIconsHomepage()
        {
            var task = new WebBrowserTask() { URL = "http://yankoa.deviantart.com" };
            task.Show();
        }
    }
}