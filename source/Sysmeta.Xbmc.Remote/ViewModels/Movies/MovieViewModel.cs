namespace Sysmeta.Xbmc.Remote.ViewModels.Movies
{
    using System;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class MovieViewModel : PropertyChangedBase
    {
        private IXbmcHost host;
        
        private string mpaa;

        private string runtime;

        public MovieViewModel(IXbmcHost host)
        {
            this.host = host;
        }

        public int Id { get; set; }

        public string GroupOn
        {
            get
            {
                char first = this.Title[0];

                if (char.IsLetter(first))
                {
                    return first.ToString();
                }

                return "0-9";
            }
        }

        public string MPAA
        {
            get
            {
                return this.mpaa;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.mpaa = null;
                }
                else
                {
                    // Remove the starting text of the rating "Rating PG-13"
                    this.mpaa = value.Substring(6);
                }
            }
        }

        private bool thumbnailResolved = false;
        private WeakReference thumbnail;
        public BitmapImage Thumbnail
        {
            get
            {
                // Images is resolved when the View asks for them
                if (!this.thumbnailResolved && this.ThumbnailSource != null)
                {
                    this.host.LoadImage(this.ThumbnailSource, image =>
                        {
                            this.thumbnailResolved = true;
                            this.Thumbnail = image;
                        });
                }

                if (this.thumbnail == null)
                {
                    return null;
                }

                return (BitmapImage)this.thumbnail.Target;
            }

            set
            {
                this.thumbnail = new WeakReference(value);
                NotifyOfPropertyChange(() => this.Thumbnail);
            }
        }

        public Uri ThumbnailSource { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public string Runtime
        {
            get
            {
                return this.runtime;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.runtime = "N/A";
                }
                else
                {
                    int x = int.Parse(value);
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
        }

        public float Rating { get; set; }

        public int PlayCount { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Tagline { get; set; }
    }
}