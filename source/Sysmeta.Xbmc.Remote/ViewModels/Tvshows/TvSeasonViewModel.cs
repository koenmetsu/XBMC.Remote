namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class TvSeasonViewModel : PropertyChangedBase
    {
        private readonly IXbmcHost host;

        public TvSeasonViewModel(IXbmcHost host)
        {
            this.host = host;
        }

        public int TvshowId { get; set; }

        public string Title { get; set; }

        private bool thumbnailResolved = false;
        private BitmapImage thumbnail;
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

                return this.thumbnail;
            }

            set
            {
                this.thumbnail = value;
                NotifyOfPropertyChange(() => this.Thumbnail);
            }
        }

        public Uri ThumbnailSource { get; set; }

        public int Episodes { get; set; }

        public int Season { get; set; }
    }
}