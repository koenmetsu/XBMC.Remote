using Microsoft.Phone.Controls;

namespace Sysmeta.Xbmc.Remote.Views.Movies
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Telerik.Windows.Data;

    public partial class MovieTitleListView : PhoneApplicationPage
    {
        public MovieTitleListView()
        {
            InitializeComponent();

            this.ListBox.GroupDescriptors.Add(new PropertyGroupDescriptor("GroupOn"));
        }

        private void ContextMenu_Unloaded(object sender, RoutedEventArgs e)
        {
            ListBox.IsEnabled = true;
        }

        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox.IsEnabled = false;
        }
    }
}