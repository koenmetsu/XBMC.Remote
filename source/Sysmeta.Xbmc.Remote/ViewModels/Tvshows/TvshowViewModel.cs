namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Model;
    using Sysmeta.Xbmc.Remote.Services;

    public class TvshowViewModel : PropertyChangedBase
    {
        private readonly Tvshow tvshow;

        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        public TvshowViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
        }

        internal TvshowViewModel(IXbmcHost host, INavigationService navigationService, Tvshow tvshow)
            : this(host, navigationService)
        {
            this.tvshow = tvshow;
        }

        private int id = -1;
        public int Id
        {
            get
            {
                if (this.tvshow == null)
                {
                    return this.id;
                }

                if (this.id == -1)
                {
                    this.id = this.tvshow.Id;
                }

                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Title
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Title;
            }
        }

        private bool thumbnailResolved = false;
        private BitmapImage thumbnail;
        public BitmapImage Thumbnail
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                // Images is resolved when the View asks for them
                if (!this.thumbnailResolved && this.ThumbnailSource != null && this.tvshow.ThumbnailImage == null)
                {
                    this.host.LoadImage(this.ThumbnailSource, image =>
                        {
                            this.thumbnailResolved = true;
                            this.tvshow.ThumbnailImage = image;
                            this.Thumbnail = image;
                        });
                }

                if (this.tvshow.ThumbnailImage != null)
                {
                    return this.tvshow.ThumbnailImage;
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
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Thumbnail;
            }
        }

        public string Plot
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Plot;
            }
        }

        public string Genre
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Genre;
            }
        }

        public float Rating
        {
            get
            {
                if (this.tvshow == null)
                {
                    return 0;
                }

                return this.tvshow.Rating;
            }
        }

        public int Episodes
        {
            get
            {
                if (this.tvshow == null)
                {
                    return 0;
                }

                return this.tvshow.Episode;
            }
        }

        public BitmapImage Fanart { get; set; }

        public Uri FanartSource
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Fanart;
            }
        }

        public int PlayCount
        {
            get
            {
                if (this.tvshow == null)
                {
                    return 0;
                }

                return this.tvshow.PlayCount;
            }
        }

        public string Studio
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Studio;
            }
        }

        public string Premiered
        {
            get
            {
                if (this.tvshow == null)
                {
                    return null;
                }

                return this.tvshow.Premiered.ToString();
            }
        }
    }
}