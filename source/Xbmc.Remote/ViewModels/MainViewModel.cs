using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Telerik.Windows.Controls;
using Xbmc.Remote.Extensions;
using Xbmc.Remote.Views;

namespace Xbmc.Remote.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        private DateTime lastLog;

        private AppState appState;

        private ApplicationSettings settings;

        private static MainViewModel instance;

        private RadPhoneApplicationFrame mainFrame;

        private List<NavigationInfo> homePageLinks;

        private NavigationInfo currentNavigationInfo;

        public MainViewModel()
        {
            Log("Create MainViewModel");

            this.homePageLinks = new List<NavigationInfo>()
            {
            	new NavigationInfo("/Xbmc.Movies;component/Movies.xaml", "movies")
            };
        }

        public static MainViewModel Instance
        {
            get
            {
        		if (instance == null)
                {
                    instance = new MainViewModel();
                }

                return instance;
            }
        }

        public RadPhoneApplicationFrame MainFrame 
        {
            get
            {
                return this.mainFrame;
            }
            set
            {
                this.mainFrame = value;
            }
        }

        public IEnumerable<NavigationInfo> HomePageLinks
        {
        	get
            {
                return this.homePageLinks;
            }
        }

        public XbmcPage CurrentPage { get; set; }

        public bool NavigateTo(NavigationInfo info)
        {
            if (string.IsNullOrEmpty(info.PageUri))
            {
                Debug.Assert(false, "No page Uri specified.");
                return false;
            }

            // cannot load the same page again
            if (this.mainFrame.Source.OriginalString == info.PageUri)
            {
                return false;
            }

            this.currentNavigationInfo = info;
            return this.mainFrame.Navigate(new Uri(info.PageUri, UriKind.Relative));
        }

        public void OnApplicationLaunching()
        {
            Log("OnApplicationLaunching");
            // Code to execute when the application is launching (eg, from Start)
            // This code will not execute when the application is reactivated
            this.appState = AppState.Launched;
        }

        public void OnApplicationActivated()
        {
            Log("OnApplicationActivated");
            // Code to execute when the application is activated (brought to foreground)
            // This code will not execute when the application is first launched
            this.appState = AppState.Activated;

            // TODO : Do I need to load some data when 
        }

        public void OnApplicationDeactivated()
        {
            Log("OnApplicationDeactivated");
            // Code to execute when the application is deactivated (sent to background)
            // This code will not execute when the application is closing
            this.appState = AppState.Deactivating;
            //this.SaveSettings();
        }

        public void OnApplicationClosing(ClosingEventArgs e)
        {
            // Code to execute when the application is closing (eg, user hit Back)
            // This code will not execute when the application is deactivated
            this.appState = AppState.Closing;
            //this.SaveSettings();
        }

        public void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs args)
        {
            // Code to execute on Unhandled Exceptions
        	// TODO : Send an issue to github or something...
        }

        public void Log(string message)
        {
            Debug.WriteLine("VM [{0}] {1}", DateTime.Now - lastLog, message);
            this.lastLog = DateTime.Now;
        }

        private void LoadSettings()
        {
            Log("LoadSettings started");

            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists("settings.json"))
                {
                    Log("Deserialize settings");
                    this.settings = JsonConvert.DeserializeObject<ApplicationSettings>(FileEx.ReadAllText(storage, "settings.json"));
                    Log("Deserialize settings done");
                }
                else
                {
                    Log("Create new settings");
                    this.settings = new ApplicationSettings();
                }
            }

            Log("LoadSettings end");
        }
    }
  
    public class NavigationInfo
    {
        public NavigationInfo(string pageUri, string text)
        {
            this.PageUri = pageUri;
            this.Text = text;
            this.Margin = new Thickness(10, 0, 0, 0);
        }

        public string PageUri { get; set; }

        public string Text { get; set; }

        public Thickness Margin { get; set; }
    }
  
    public enum AppState
    {
        Launched,
        Activated,
        Deactivating,
        Closing
    }
}
