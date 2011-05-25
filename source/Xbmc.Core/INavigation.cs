namespace Xbmc.Core
{
    using Telerik.Windows.Controls;

    public interface INavigation
    {
        RadPhoneApplicationFrame MainFrame { get; set; }

        NavigationInfo CurrentNavigationInfo { get; set; }

        bool NavigateTo(NavigationInfo info);
    }
}