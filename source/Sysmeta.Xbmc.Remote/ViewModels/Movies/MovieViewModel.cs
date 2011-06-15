namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Model;
    using Sysmeta.Xbmc.Remote.Services;

    public class MovieViewModel : Screen
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        private Movie movie;

        public MovieViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
        }

        internal MovieViewModel(IXbmcHost host, INavigationService navigationService, Movie movie)
            : this(host, navigationService)
        {
            this.movie = movie;
        }

        private int id = -1;
        public int Id
        {
            get
            {
                if (this.movie == null)
                {
                    return id;
                }

                if (this.id == -1)
                {
                    this.id = this.movie.Id;
                }

                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public string GroupOn
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                char first = this.Title[0];

                if (char.IsLetter(first))
                {
                    return first.ToString();
                }

                return "0-9";
            }
        }

        private string mpaa;
        public string MPAA
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                if (this.mpaa == null && this.movie.MPAA != null)
                {
                    // Remove the starting text of the rating "Rating PG-13"
                    this.mpaa = this.movie.MPAA.Substring(6);
                }

                return this.mpaa;
            }
        }

        private bool thumbnailResolved = false;
        private BitmapImage thumbnail;
        public BitmapImage Thumbnail
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                // Images is resolved when the View asks for them
                if (!this.thumbnailResolved && this.ThumbnailSource != null && this.movie.ThumbnailImage == null)
                {
                    this.host.LoadImage(this.ThumbnailSource, image =>
                        {
                            this.thumbnailResolved = true;
                            this.movie.ThumbnailImage = image;
                            this.Thumbnail = image;
                        });
                }

                if (this.movie.ThumbnailImage != null)
                {
                    this.thumbnail = this.movie.ThumbnailImage;
                    this.thumbnailResolved = true;
                }

                if (this.thumbnail == null)
                {
                    return null;
                }

                return this.thumbnail;
            }

            set
            {
                this.thumbnail = value;
                NotifyOfPropertyChange(() => this.Thumbnail);
            }
        }

        public Uri ThumbnailSource
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }
             
                return this.movie.Thumbnail;
            }
        }

        public string Title
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                return this.movie.Title;
            }
        }

        private string year;
        public string Year
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                if (this.year == null)
                {
                    this.year = movie.Year == 0 ? "N/A" : movie.Year.ToString();
                }

                return this.year;
            }
        }

        private string runtime;
        public string Runtime
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                if (this.runtime == null)
                {
                    if (string.IsNullOrEmpty(this.movie.Runtime))
                    {
                        this.runtime = "N/A";
                    }
                    else
                    {
                        int x = int.Parse(this.movie.Runtime);
                        int h = x / 60;
                        int m = x % 60;

                        string time;
                        if (h == 0 && m == 0)
                        {
                            time = "N/A";
                        }
                        else if (m == 0)
                        {
                            time = string.Format("{0} h", h);
                        }
                        else if (h == 0)
                        {
                            time = string.Format("{0} min", m);
                        }
                        else
                        {
                            time = string.Format("{0} h {1} min", h, m);
                        }

                        this.runtime = time;
                    }
                }

                return this.runtime;
            }
        }

        public float Rating
        {
            get
            {
                if (this.movie == null)
                {
                    return 0;
                }

                return this.movie.Rating;
            }
        }

        public int PlayCount
        {
            get
            {
                if (this.movie == null)
                {
                    return 0;
                }

                return this.movie.PlayCount;
            }
        }

        public string Genre
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                return this.movie.Genre;
            }
        }

        public string Director
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                return this.movie.Director;
            }
        }

        public string Tagline
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                return this.movie.Tagline;
            }
        }

        public string Plot
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                return this.movie.Plot;
            }
        }

        public string Studio
        {
            get
            {
                if (this.movie == null)
                {
                    return null;
                }

                return this.movie.Studio;
            }
        }

        public void Play()
        {
            this.host.PlayMovie(this.Id);
        }

        public void Queue()
        {

        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (this.movie == null)
            {
                this.host.GetMovieDetails(
                    this.Id,
                    movie =>
                        {
                            if (movie == null)
                            {
                                this.navigationService.GoBack();
                                return;
                            }

                            this.movie = movie;

                            this.NotifyOfPropertyChange(() => this.Title);
                            this.NotifyOfPropertyChange(() => this.Studio);
                            this.NotifyOfPropertyChange(() => this.Plot);
                            this.NotifyOfPropertyChange(() => this.Year);
                            this.NotifyOfPropertyChange(() => this.Director);
                            this.NotifyOfPropertyChange(() => this.MPAA);
                            this.NotifyOfPropertyChange(() => this.Runtime);
                            this.NotifyOfPropertyChange(() => this.Rating);
                            this.NotifyOfPropertyChange(() => this.PlayCount);
                            this.NotifyOfPropertyChange(() => this.Genre);
                            this.NotifyOfPropertyChange(() => this.Tagline);
                            this.NotifyOfPropertyChange(() => this.Thumbnail);
                        });
            }
        }
    }
}