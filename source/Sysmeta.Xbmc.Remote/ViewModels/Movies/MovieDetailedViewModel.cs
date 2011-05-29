namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class MovieDetailedViewModel : Screen
    {
        private readonly IXbmcHost host;

        public MovieDetailedViewModel(IXbmcHost host)
        {
            this.host = host;
        }

        public string Title { get; set; }

        public int MovieId { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetMovieDetail(this.MovieId, movie =>
                {
                    
                });
        }
    }
}