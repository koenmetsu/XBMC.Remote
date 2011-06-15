namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    public class TvEpisodeViewDesignData
    {
        public TvEpisodeViewDesignData()
        {
            this.Title = "Chuck Versus the Intersect";
            this.Plot = "Chuck Bartowski is an average computer geek until files of government secrets are downloaded into his brain. He is soon scouted by the CIA and NSA to act in place of their computer.";
            this.Rating = 7.699999809265137f;
            this.Director = "McG";
            this.Duration = "43 min";
            this.SeasonString = "01";
            this.EpisodeString = "01";
            this.PlayCount = 1;
            this.MPAA = "TV-PG";
            this.Writer = "Josh Schwartz / Chris Fedak";
            this.Studio = "NBC";
            this.FirstAired = "2007-09-24";
        }

        public string Title { get; set; }

        public string Plot { get; set; }

        public string Director { get; set; }

        public float Rating { get; set; }

        public string Duration { get; set; }

        public string SeasonString { get; set; }

        public string EpisodeString { get; set; }

        public int PlayCount { get; set; }

        public string MPAA { get; set; }

        public string Writer { get; set; }

        public string Studio { get; set; }

        public string FirstAired { get; set; }
    }
}