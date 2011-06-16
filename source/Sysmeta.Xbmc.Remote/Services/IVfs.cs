namespace Sysmeta.Xbmc.Remote.Services
{
    using System;

    public interface IVfs
    {
        void GetFile(Uri uri, Action<byte[], Exception> callback);
    }
}