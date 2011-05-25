namespace Xbmc.Core
{
    using System;
    using System.Diagnostics;

    using Telerik.Windows.Controls;

    internal class Navigator : INavigation
    {
        public RadPhoneApplicationFrame MainFrame { get; set; }

        public NavigationInfo CurrentNavigationInfo { get; set; }

        public bool NavigateTo(NavigationInfo info)
        {
            if (string.IsNullOrEmpty(info.PageUri))
            {
                Debug.Assert(false, "No page Uri specified.");
                return false;
            }

            // cannot load the same page again
            if (this.MainFrame.Source.OriginalString == info.PageUri)
            {
                return false;
            }

            this.CurrentNavigationInfo = info;
            return this.MainFrame.Navigate(new Uri(info.PageUri, UriKind.Relative));
        }
    }
}