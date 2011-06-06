namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    public class TvEpisodeListItemDesignData
    {
        public TvEpisodeListItemDesignData()
        {
            this.Title = "Chuck Versus the Intersect";
            this.Season = 1;
            this.Episode = 1;
            this.Rating = 8.1f;
        }

        public string Title { get; set; }

        public int Episode { get; set; }

        public int Season { get; set; }

        public float Rating { get; set; }
    }
}