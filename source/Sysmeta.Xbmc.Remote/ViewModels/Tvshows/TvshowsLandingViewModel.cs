using System;
using System.Collections.Generic;

using Caliburn.Micro;
using Sysmeta.Xbmc.Remote.Services;

namespace Sysmeta.Xbmc.Remote.ViewModels.Tvshows
{
    using System.Linq;

    using Telerik.Windows.Controls;

    public class TvshowsLandingViewModel : Screen, IMenuItem
    {
        private readonly IXbmcHost host;

        private readonly INavigationService navigationService;

        public static string TitleString = "tvshows";

        public TvshowsLandingViewModel(IXbmcHost host, INavigationService navigationService)
        {
            this.host = host;
            this.navigationService = navigationService;
            this.Title = TitleString;
            this.Description = "browse your tv shows library";
            this.Image = "/Sysmeta.Xbmc.Remote;component/Images/{0}/tv.png";
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IEnumerable<TvshowViewModel> Shows { get; set; }

        public void Selected(ListBoxItemTapEventArgs eventArgs)
        {
            var show = (TvshowViewModel)eventArgs.Item.Content;

            this.navigationService.UriFor<TvshowSeasonsViewModel>()
                .WithParam(p => p.TvShowId, show.Id)
                .WithParam(p => p.Title, show.Title)
                .Navigate();
        }


        protected override void OnActivate()
        {
            base.OnActivate();

            host.GetTvshows(shows =>
                            {
                                if (shows == null)
                                {
                                    this.navigationService.GoBack();
                                    return;
                                }

                                this.Shows = shows.Select(s => new TvshowViewModel(host, navigationService, s)).ToList();
                                NotifyOfPropertyChange(() => this.Shows);
                            });
        }
    }
}