namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using Caliburn.Micro;

    public class MovieListDesignData
    {
        public MovieListDesignData()
        {
            this.Movies = new BindableCollection<MovieDesignData>()
                {
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                    new MovieDesignData(),
                };
        }

        public IObservableCollection<MovieDesignData> Movies { get; set; }
    }
}