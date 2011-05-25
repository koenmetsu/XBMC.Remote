namespace Xbmc.Core
{
    using System.Windows;

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
}