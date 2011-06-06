namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;

    using Sysmeta.Xbmc.Remote.Services;

    public class TvEpisodeViewModel
    {
        private readonly IXbmcHost host;

        public TvEpisodeViewModel(IXbmcHost host)
        {
            this.host = host;
        }

        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public int Year { get; set; }

        
        public float Rating { get; set; }

        
        public string Director { get; set; }

        
        public string Plot { get; set; }

        
        public string LastPlayed { get; set; }

        
        public string ShowTitle { get; set; }

        
        public string FirstAired { get; set; }

        
        public int Duration { get; set; }

        
        public int Season { get; set; }

        
        public int Episode { get; set; }

        
        public int PlayCount { get; set; }

        
        public string Writer { get; set; }

        
        public string Studio { get; set; }

        
        public string MPAA { get; set; }

        
        public string Premiered { get; set; }

        public Uri FanartSource { get; set; }

        public void Play()
        {
            this.host.PlayEpisode(this.Id);
        }
    }
}