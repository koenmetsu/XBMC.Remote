using System;
using Caliburn.Micro;

namespace Sysmeta.Xbmc.Remote.ViewModels.Remote
{
    public class RemoteViewModel : Screen, IMenuItem
    {
        public RemoteViewModel()
        {
            this.Title = "remote";
            this.Description = "remote control bla bla bla";
            this.Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/remote.png", UriKind.RelativeOrAbsolute);
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public System.Uri Image { get; set; }
    }
}