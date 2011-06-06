namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class TvEpisodeListViewModel : Screen
    {
        private readonly IXbmcHost host;

        public TvEpisodeListViewModel(IXbmcHost host)
        {
            this.host = host;
        }

        public int Season { get; set; }

        public int TvshowId { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetTvEpisodes(this.TvshowId, this.Season, models =>
                {
                    
                });
        }
    }
}