namespace Xbmc.Remote.Views
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Navigation;

    using Microsoft.Phone.Controls;

    using Xbmc.Core;
    using Xbmc.Remote.ViewModels;

    public class XbmcPage : PhoneApplicationPage
    {
        public XbmcPage()
        {
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        public bool IsLoaded { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            MainViewModel.Instance.CurrentPage = this;

            this.NavigationContext.QueryString
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            var context = FindDataContext<NavigationInfo>(e.OriginalSource as DependencyObject);
            if (context != null)
            {
                MainViewModel.Instance.NavigateTo(context);
            }
        }

        private static T FindDataContext<T>(DependencyObject target) where T : class
        {
            while (target != null)
            {
                var element = target as FrameworkElement;
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