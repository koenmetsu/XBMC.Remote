namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Services;

    public class ConnectionsViewModel : Screen, IMenuItem
    {
        private readonly INavigationService navigationService;

        private readonly SettingsHost settingsHost;

        public static string TitleString = "connections";

        public ConnectionsViewModel(INavigationService navigationService, SettingsHost settingsHost)
        {
            this.navigationService = navigationService;
            this.settingsHost = settingsHost;
            this.Title = TitleString;
            this.Description = "list of XBMC connections";
            this.Image = "/Sysmeta.Xbmc.Remote;component/Images/{0}/servers.png";

            LoadConnections();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public IEnumerable<ConnectionViewModel> Connections { get; set; }

        public void Add()
        {
            this.navigationService.UriFor<ConnectionViewModel>().Navigate();
        }

        public void Selected(ConnectionViewModel connection)
        {
            this.navigationService.UriFor<ConnectionViewModel>()
                .WithParam(c => c.MachineAddress, connection.MachineAddress)
                .WithParam(c => c.Port, connection.Port)
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            LoadConnections();
        }

        private void LoadConnections()
        {
            this.Connections = this.settingsHost.Settings.Connections.Select(c => new ConnectionViewModel(this.navigationService, settingsHost, c, () => this.LoadConnections())).OrderByDescending(model => model.IsActive).ToList();
            this.NotifyOfPropertyChange(() => this.Connections);
        }
    }


}