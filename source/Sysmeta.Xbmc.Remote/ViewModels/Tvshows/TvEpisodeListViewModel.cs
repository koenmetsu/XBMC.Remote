namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class TvEpisodeListViewModel : Screen
    {
        private readonly IXbmcHost host;

        public TvEpisodeListViewModel(IXbmcHost host)
        {
            this.host = host;
        }

        public string SeasonTitle { get; set; }

        public string TvshowName { get; set; }

        public int Season { get; set; }

        public int TvshowId { get; set; }

        public IEnumerable<TvEpisodeViewModel> Episodes { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.host.GetTvEpisodes(this.TvshowId, this.Season, episodes =>
                {
                    this.Episodes = episodes.OrderBy(e => e.Episode).ToList();

                    this.SeasonTitle = string.Format("Season {0}", this.Season);
                    this.TvshowName = this.Episodes.First().ShowTitle;

                    NotifyOfPropertyChange(() => this.Episodes);
                    NotifyOfPropertyChange(() => this.SeasonTitle);
                    NotifyOfPropertyChange(() => this.TvshowName);
                });
        }
    }
}