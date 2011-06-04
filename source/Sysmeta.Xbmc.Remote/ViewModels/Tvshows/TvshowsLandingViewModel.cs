using System;
using Caliburn.Micro;

namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    public class TvshowsLandingViewModel : Screen, IMenuItem
    {
        public TvshowsLandingViewModel()
        {
            this.Title = "tvshows";
            this.Description = "tv shows are awsome";
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/tv.png", UriKind.RelativeOrAbsolute);
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public System.Uri Image { get; set; }
    }
}