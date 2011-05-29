namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System;
    using System.Collections.Generic;

    public class MainMenuDesignData
    {
        public MainMenuDesignData()
        {
            MenuItems = new[]
                {
                    new MenuItemDesignData() { Title = "movies", Description = "testing", Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/movies.png", UriKind.RelativeOrAbsolute) },
                    new MenuItemDesignData() { Title = "tvshows", Description = "testing", Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/tv.png", UriKind.RelativeOrAbsolute) },
                    new MenuItemDesignData() { Title = "remote", Description = "testing", Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/remote.png", UriKind.RelativeOrAbsolute) },
                    new MenuItemDesignData() { Title = "settings", Description = "testing", Image = new Uri("/Sysmeta.Xbmc.Remote;component/Images/Black/settings.png", UriKind.RelativeOrAbsolute) },
                };
        }

        public IEnumerable<MenuItemDesignData> MenuItems { get; set; } 
    }

    public class MenuItemDesignData
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }
    }
}