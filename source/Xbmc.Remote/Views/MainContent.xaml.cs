using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using Xbmc.Remote.ViewModels;
using Xbmc.Remote.Views;

namespace Xbmc.Remote
{
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
            InitializeComponent();
        }

        public void HomePageLinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel.Instance.CurrentPage.SetValue(RadTransitionControl.TransitionProperty, new RadTileTransition());
            MainViewModel.Instance.CurrentPage.SetValue(RadTileAnimation.ContainerToAnimateProperty, HomePageLinks);
        }
    }
}