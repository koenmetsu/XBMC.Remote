namespace Xbmc.Core
{
    using System.Windows;

    public interface IUnhandledException
    {
        void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e);
    }
}