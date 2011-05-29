namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System.Collections.Generic;

    public class MoviesLandingDesignData
    {
        public MoviesLandingDesignData()
        {
            this.Items = new[]
                {
                    new MoviesLandingItemDesignData() { Title = "genres", Description = "choose what movie genere to view" },
                    new MoviesLandingItemDesignData() { Title = "title", Description = "sort movies by there title" },
                    new MoviesLandingItemDesignData() { Title = "year", Description = "choose what ..." }
                };
        }

        public IEnumerable<MoviesLandingItemDesignData> Items { get; set; }
    }

    public class MoviesLandingItemDesignData
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}