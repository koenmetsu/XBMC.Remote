namespace Sysmeta.Xbmc.Remote 
{
    using Sysmeta.Xbmc.Remote.ViewModels;

    public class MainPageViewModel
    {
        public MainPageViewModel(MainMenuViewModel mainMenu, RecentlyAddedMoviesViewModel recentlyAddedMovies)
        {
            this.MainMenu = mainMenu;
            this.RecentlyAddedMovies = recentlyAddedMovies;
        }

        public MainMenuViewModel MainMenu { get; set; }

        public RecentlyAddedMoviesViewModel RecentlyAddedMovies { get; set; }
    }
}
