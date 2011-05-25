namespace Xbmc.Core
{
    using Microsoft.Phone.Shell;

    public interface IApplicationClosing
    {
        void OnApplicationClosing(ClosingEventArgs e);
    }
}