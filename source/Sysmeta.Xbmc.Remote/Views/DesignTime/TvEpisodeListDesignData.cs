namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System.Collections.Generic;

    public class TvEpisodeListDesignData
    {
        public TvEpisodeListDesignData()
        {
            this.Episodes = new[]
                {
                    new TvEpisodeListItemDesignData() { Episode = 1}, new TvEpisodeListItemDesignData(){ Episode = 2}, new TvEpisodeListItemDesignData() { Episode = 11},
                    new TvEpisodeListItemDesignData(), new TvEpisodeListItemDesignData(), new TvEpisodeListItemDesignData(),
                    new TvEpisodeListItemDesignData(),
                };

            this.SeasonTitle = "Season 1";
            this.TvshowName = "Bones";
        }

        public string SeasonTitle { get; set; }

        public string TvshowName { get; set; }

        public IEnumerable<TvEpisodeListItemDesignData> Episodes { get; set; }
    }
}