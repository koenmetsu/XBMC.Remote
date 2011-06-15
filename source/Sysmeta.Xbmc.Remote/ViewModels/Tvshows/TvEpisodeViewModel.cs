namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System;
    using System.Linq;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Model;
    using Sysmeta.Xbmc.Remote.Services;

    public class TvEpisodeViewModel : Screen
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        private TvEpisode episode;

        public TvEpisodeViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
        }

        internal TvEpisodeViewModel(IXbmcHost host, INavigationService navigationService, TvEpisode episode)
            : this(host, navigationService)
        {
            this.episode = episode;
        }

        public int TvshowId { get; set; }

        private int id = -1;
        public int Id
        {
            get
            {
                if (this.episode == null)
                {
                    return this.id;
                }

                if (this.id == -1)
                {
                    this.id = this.episode.Id;
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
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Title;
            }
        }

        private string year;
        public string Year
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                if (this.year == null)
                {
                    if (this.episode.Year == 0)
                    {
                        this.year = "N/A";
                    }
                    else
                    {
                        this.year = this.episode.Year.ToString();
                    }
                }

                return this.year;
            }
        }


        public float Rating
        {
            get
            {
                if (this.episode == null)
                {
                    return 0;
                }

                return this.episode.Rating;
            }
        }


        public string Director
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Director;
            }
        }


        public string Plot
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Plot;
            }
        }


        public string LastPlayed
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.LastPlayed;
            }
        }

        public string ShowTitle
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.ShowTitle;
            }
        }

        public string FirstAired
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.FirstAired;
            }
        }


        private string duration;
        public string Duration
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                int duration = this.episode.Duration;

                if (duration == 0)
                {
                    return "N/A";
                }

                int h = duration / 3600;
                int m = (duration % 3600) / 60;
                
                if (h == 0 && m > 0)
                {
                    return string.Format("{0} min", m);
                }

                return string.Format("{0} h {1} min", h, m);
            }
        }

        private int season = -1;
        public int Season
        {
            get
            {
                if (this.episode == null)
                {
                    return 0;
                }

                if (this.season == -1)
                {
                    this.season = this.episode.Season;
                }

                return this.season;
            }

            set
            {
                this.season = value;
            }
        }

        public string SeasonString
        {
            get
            {
                return string.Format("{0:00}", this.season);
            }
        }

        private int episodeNr = -1;
        public int Episode
        {
            get
            {
                if (this.episode == null)
                {
                    return 0;
                }

                if (this.episodeNr == -1)
                {
                    this.episodeNr = this.episode.Episode;
                }

                return this.episodeNr;
            }

            set
            {
                this.episodeNr = value;
            }
        }

        public string EpisodeString
        {
            get
            {
                return string.Format("{0:00}", this.episodeNr);
            }
        }

        public int PlayCount
        {
            get
            {
                if (this.episode == null)
                {
                    return 0;
                }

                return this.episode.PlayCount;
            }
        }


        public string Writer
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Writer;
            }
        }


        public string Studio
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Studio;
            }
        }


        public string MPAA
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.MPAA;
            }
        }

        public string Premiered
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Premiered;
            }
        }

        public Uri FanartSource
        {
            get
            {
                if (this.episode == null)
                {
                    return null;
                }

                return this.episode.Fanart;
            }
        }

        public void Play()
        {
            this.host.PlayEpisode(this.Id);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (this.episode == null)
            {
                this.host.GetTvEpisodes(this.TvshowId, this.season, episodes =>
                    {
                        var ep = episodes.Where(e => e.Episode == this.episodeNr).FirstOrDefault();
                        if (ep == null)
                        {
                            this.navigationService.GoBack();
                        }

                        this.episode = ep;
                        NotifyOfPropertyChange(() => this.Title);
                        NotifyOfPropertyChange(() => this.Year);
                        NotifyOfPropertyChange(() => this.Rating);
                        NotifyOfPropertyChange(() => this.Director);
                        NotifyOfPropertyChange(() => this.Plot);
                        NotifyOfPropertyChange(() => this.LastPlayed);
                        NotifyOfPropertyChange(() => this.ShowTitle);
                        NotifyOfPropertyChange(() => this.FirstAired);
                        NotifyOfPropertyChange(() => this.Duration);
                        NotifyOfPropertyChange(() => this.Season);
                        NotifyOfPropertyChange(() => this.Episode);
                        NotifyOfPropertyChange(() => this.PlayCount);
                        NotifyOfPropertyChange(() => this.Writer);
                        NotifyOfPropertyChange(() => this.Studio);
                        NotifyOfPropertyChange(() => this.MPAA);
                        NotifyOfPropertyChange(() => this.Premiered);
                    });
            }
        }
    }
}