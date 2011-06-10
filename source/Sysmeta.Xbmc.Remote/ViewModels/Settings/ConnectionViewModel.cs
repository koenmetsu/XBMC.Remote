namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Model;
    using Sysmeta.Xbmc.Remote.Services;

    public class ConnectionViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly SettingsHost settingsHost;

        private Connection connection;

        public ConnectionViewModel(INavigationService navigationService, SettingsHost settingsHost)
        {
            this.navigationService = navigationService;
            this.settingsHost = settingsHost;
            this.Port = "8080";
        }

        internal ConnectionViewModel(INavigationService navigationService, SettingsHost settingsHost, Connection connection)
            : this (navigationService, settingsHost)
        {
            this.connection = connection;

            this.MachineAddress = this.connection.Url.ToString();
        }

        public string MachineAddress { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Error { get; set; }

        public void Save()
        {
            this.settingsHost.AddConnection(new Connection
                {
                    Url = new Uri(string.Format("http://{0}:{1}", MachineAddress, Port)),
                    Username = this.Username,
                    Password = this.Password
                });

            this.navigationService.GoBack();
        }

        public void Cancel()
        {
            this.navigationService.GoBack();
        }
    }
}