namespace Sysmeta.Xbmc.Remote.Services
{
    public interface IXbmcClient
    {
        IVideoLibrary Video { get; }

        IVfs Vfs { get; }

        void PlayMovie(int id);

        void PlayEpisode(int id);
    }
}