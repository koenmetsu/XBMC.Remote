using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Xbmc.Remote.ViewModels;

namespace Xbmc.Remote.Views
{
    public class XbmcPage : PhoneApplicationPage
    {
        public XbmcPage()
        {
            this.Loaded += OnLoaded;
            this.Unloaded += OnUnloaded;
        }

        public bool IsLoaded { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            MainViewModel.Instance.CurrentPage = this;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            NavigationInfo context = FindDataContext<NavigationInfo>(e.OriginalSource as DependencyObject);
            if (context != null)
            {
                MainViewModel.Instance.NavigateTo(context);
            }
        }

        public static T FindDataContext<T>(DependencyObject target) where T : class
        {
            while (target != null)
            {
                FrameworkElement element = target as FrameworkElement;
                if (element != null && element.DataContext is T)
                {
                    return element.DataContext as T;
                }

                target = VisualTreeHelper.GetParent(target);
            }

            return null;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.IsLoaded = true;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.IsLoaded = false;
        }
    }
}
