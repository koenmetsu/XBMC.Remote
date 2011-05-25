namespace Xbmc.Remote
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Navigation;

    using Microsoft.Phone.Shell;

    using Telerik.Windows.Controls;

    using TinyIoC;

    using Xbmc.Core;

    public partial class App : Application
    {
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            this.UnhandledException += this.Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            this.InitializeComponent();

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            this.RootFrame = new RadPhoneApplicationFrame();

            this.RootFrame.Navigated += this.OnRootFrameNavigated;

            // Handle navigation failures
            this.RootFrame.NavigationFailed += this.RootFrame_NavigationFailed;

            this.RootFrame.Style = this.Resources["RadPhoneApplicationFrameStyle"] as Style;

            this.Bootstraper = new XbmcBootstrapper(this.RootFrame);
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public RadPhoneApplicationFrame RootFrame { get; private set; }

        public XbmcBootstrapper Bootstraper { get; set; }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            var handler = TinyIoCContainer.Current.Resolve<IApplicationLaunching>();
            if (handler != null)
            {
                handler.OnApplicationLaunching();
            }
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            var handler = TinyIoCContainer.Current.Resolve<IApplicationActivated>();
            if (handler != null)
            {
                handler.OnApplicationActivated();
            }
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            var handler = TinyIoCContainer.Current.Resolve<IApplicationDeactivated>();
            if (handler != null)
            {
                handler.OnApplicationDeactivated();
            }
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            var handler = TinyIoCContainer.Current.Resolve<IApplicationClosing>();
            if (handler != null)
            {
                handler.OnApplicationClosing(e);
            }
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            var handler = TinyIoCContainer.Current.Resolve<INavigationFailed>();
            if (handler != null)
            {
                handler.OnNavigationFailed(sender, e);
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            var handler = TinyIoCContainer.Current.Resolve<IUnhandledException>();
            if (handler != null)
            {
                handler.OnUnhandledException(sender, e);
            }
        }

        #region Phone application initialization

        // Do not add any additional code to this method
        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            Current.RootVisual = this.RootFrame;
            this.RootFrame.Background = (Brush)Current.Resources["InnerScreensBrush"];

            this.RootFrame.Navigated -= this.OnRootFrameNavigated;
            TinyIoCContainer.Current.Resolve<INavigation>().MainFrame = this.RootFrame;
        }

        #endregion
    }
}