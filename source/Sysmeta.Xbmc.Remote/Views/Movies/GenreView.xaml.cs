using System.Windows;

using Microsoft.Phone.Controls;

namespace Sysmeta.Xbmc.Remote.Views.Movies
{
    public partial class GenreView : PhoneApplicationPage
    {
        public GenreView()
        {
            InitializeComponent();
        }

        private void ContextMenuUnloaded(object sender, RoutedEventArgs e)
        {
            ListBox.IsEnabled = true;
        }

        private void ContextMenuLoaded(object sender, RoutedEventArgs e)
        {
            ListBox.IsEnabled = false;
        }
    }
}