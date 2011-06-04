namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System.Collections.Generic;

    public class MovieGenresDesignData
    {
        public MovieGenresDesignData()
        {
            Genres = new[]
                {
                    new MovieGenreDesignData { Name = "action" }, new MovieGenreDesignData { Name = "adventure" },
                    new MovieGenreDesignData { Name = "animation" }, new MovieGenreDesignData { Name = "biography" },
                    new MovieGenreDesignData { Name = "comedy" }, new MovieGenreDesignData { Name = "crime" },
                    new MovieGenreDesignData { Name = "disaster" }, new MovieGenreDesignData { Name = "documentary" },
                    new MovieGenreDesignData { Name = "drama" }, new MovieGenreDesignData { Name = "eastern" },
                    new MovieGenreDesignData { Name = "family" }, new MovieGenreDesignData { Name = "fantasy" },
                };
        }

        public IEnumerable<MovieGenreDesignData> Genres { get; set; } 
    }

    public class MovieGenreDesignData
    {
        public string Name { get; set; }
    }
}