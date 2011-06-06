using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Sysmeta.Xbmc.Remote.Views.Tvshows
{
    public partial class TvEpisodeListView : PhoneApplicationPage
    {
        public TvEpisodeListView()
        {
            InitializeComponent();
        }

        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListBox.IsEnabled = false;
        }

        private void ContextMenu_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ListBox.IsEnabled = true;
        }
    }
}