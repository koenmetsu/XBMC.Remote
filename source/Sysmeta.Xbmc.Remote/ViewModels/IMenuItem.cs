namespace Sysmeta.Xbmc.Remote.ViewModels
{
    using System;

    public interface IMenuItem
    {
        string Title { get; set; }

        string Description { get; set; }

        Uri Image { get; set; }
    }
}