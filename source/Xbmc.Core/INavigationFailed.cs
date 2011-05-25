namespace Xbmc.Core
{
    using System.Windows.Navigation;

    public interface INavigationFailed
    {
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e);
    }
}