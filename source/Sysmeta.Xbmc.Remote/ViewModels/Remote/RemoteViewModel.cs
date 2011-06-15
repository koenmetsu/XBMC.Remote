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
            this.Image = "/Sysmeta.Xbmc.Remote;component/Images/{0}/remote.png";
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}