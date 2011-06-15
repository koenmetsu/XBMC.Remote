namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using Caliburn.Micro;

    public class RecentlyAddedMoviesViewDesignData
    {
        public RecentlyAddedMoviesViewDesignData()
        {
            this.Movies = new BindableCollection<MovieDesignData>()
                {
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData() { Thumbnail = null },
                    new MovieDesignData(),
                    new MovieDesignData(),
                };
        }

        public IObservableCollection<MovieDesignData> Movies { get; set; }
    }
}