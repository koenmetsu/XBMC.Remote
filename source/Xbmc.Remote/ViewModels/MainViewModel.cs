namespace Xbmc.Remote.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    using Telerik.Windows.Controls;

    using Xbmc.Core;
    using Xbmc.Remote.Views;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class MainViewModel : BaseViewModel
    {
        private readonly List<NavigationInfo> homePageLinks;

        public MainViewModel()
        {
            this.homePageLinks = new List<NavigationInfo>
                {
                    new NavigationInfo("/xbmc.movies;component/Movies.xaml", "movies"),
                    new NavigationInfo("/Xbmc.Movies;component/Movies.xaml", "tvshows"),
                    new NavigationInfo("/Xbmc.Movies;component/Movies.xaml", "remote"),
                    new NavigationInfo("/Xbmc.Movies;component/Movies.xaml", "settings")
                };
        }

        public RadPhoneApplicationFrame MainFrame { get; set; }

        public IEnumerable<NavigationInfo> HomePageLinks
        {
            get
            {
                return this.homePageLinks;
            }
        }

        public XbmcPage CurrentPage { get; set; }
    }

    public enum AppState
    {
        Launched,

        Activated,

        Deactivating,

        Closing
    }
}