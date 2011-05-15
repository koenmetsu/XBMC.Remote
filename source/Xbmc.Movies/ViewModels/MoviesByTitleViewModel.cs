namespace Xbmc_Movies
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    using Xbmc.Client;

    public class MoviesByTitleViewModel
    {
        private XbmcClient client;

        public MoviesByTitleViewModel()
        {
            this.client = new XbmcClient("http://localhost:8081/");

            this.Movies = new ObservableCollection<MovieViewModel>();
            this.client.VideoLibrary.GetMovies(this.OnGetMovies, MovieFields.Title, MovieFields.Cast, MovieFields.Director, 
                MovieFields.Genre, MovieFields.Rating, MovieFields.Year, MovieFields.Thumbnail, MovieFields.Runtime);
        }

        private void OnGetMovies(MoviesResult moviesResult, Exception exception)
        {
            foreach (var movie in moviesResult.Movies)
            {
                var movieVm = new MovieViewModel()
                    {
                        Title = movie.Title,
                        Cast = movie.Cast,
                        Director = movie.Cast,
                        Duration = movie.Runtime,
                        Genre = movie.Genre,
                        Rating = movie.Rating.ToString(),
                        Year = movie.Year
                    };

                if (movie.Thumbnail != null)
                {
                    this.LoadThumbnail(movieVm, movie.Thumbnail);
                }

                this.Movies.Add(movieVm);
            }
        }

        public ObservableCollection<MovieViewModel> Movies { get; set; }

        private void LoadThumbnail(MovieViewModel movieVm, Uri thumbnail)
        {
            this.client.Vfs.GetFile(thumbnail, (bytes, exception) =>
                {
                    if (exception == null)
                    {
                        var img = new BitmapImage();
                        img.SetSource(new MemoryStream(bytes));

                        movieVm.Thumbnail = img;
                    }
                });
        }
    }
}