namespace Sysmeta.Xbmc.Remote.Services
{
    public class DemoXbmcClient : IXbmcClient
    {
        public DemoXbmcClient()
        {
            this.Video = new DemoVideoLibrary();
            this.Vfs = new DemoVfs();
        }

        public IVideoLibrary Video { get; private set; }

        public IVfs Vfs { get; private set; }

        public void PlayMovie(int id)
        {
            
        }

        public void PlayEpisode(int id)
        {
           
        }
    }
}