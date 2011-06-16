namespace Sysmeta.Xbmc.Remote.ViewModels.Settings
{
    using System;
    using System.Linq;

    using Caliburn.Micro;

    using Sysmeta.Xbmc.Remote.Model;
    using Sysmeta.Xbmc.Remote.Services;

    using Action = System.Action;

    public class ConnectionViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly SettingsHost settingsHost;

        private Connection connection;

        private readonly Action changed;

        public ConnectionViewModel(INavigationService navigationService, SettingsHost settingsHost)
        {
            this.navigationService = navigationService;
            this.settingsHost = settingsHost;
            this.Port = "8080";
        }

        internal ConnectionViewModel(INavigationService navigationService, SettingsHost settingsHost, Connection connection, Action changed)
            : this (navigationService, settingsHost)
        {
            this.connection = connection;
            this.changed = changed;

            this.SetConnectionParameters();
        }

        public bool IsActive { get; set; }

        public string MachineAddress { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Error { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (this.MachineAddress != null)
            {
                var active =
                    this.settingsHost.Settings.Connections.Where(
                        c => c.Url == new Uri(string.Format("http://{0}:{1}", this.MachineAddress, this.Port))).FirstOrDefault();
                if (active != null)
                {
                    this.connection = active;
                    this.SetConnectionParameters();
                }
            }
        }

        public void Save()
        {
            if (this.connection != null)
            {
                this.connection.Url = new Uri(string.Format("http://{0}:{1}", MachineAddress, Port));
                this.connection.Username = this.Username;
                this.connection.Password = this.Password;

                this.settingsHost.SetActiveConnection(connection);
                
                this.settingsHost.Save();
            }
            else
            {
                this.settingsHost.AddConnection(new Connection
                {
                    Url = new Uri(string.Format("http://{0}:{1}", MachineAddress, Port)),
                    Username = this.Username,
                    Password = this.Password
                });
            }

            this.navigationService.GoBack();
        }

        public void Cancel()
        {
            this.navigationService.GoBack();
        }

        public void SetActive()
        {
            this.settingsHost.SetActiveConnection(this.connection);

            if (this.changed != null)
            {
                this.changed();
            }
        }

        public void Remove()
        {
            this.settingsHost.RemoveConnection(this.connection);
            
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public void RemoveAndNavigateBack()
        {
            this.settingsHost.RemoveConnection(this.connection);
            this.navigationService.GoBack();
        }

        private void SetConnectionParameters()
        {
            this.MachineAddress = this.connection.Url.Host;
            NotifyOfPropertyChange(() => this.MachineAddress);
            this.Port = this.connection.Url.Port.ToString();
            NotifyOfPropertyChange(() => this.Port);
            this.Username = this.connection.Username;
            NotifyOfPropertyChange(() => this.Username);
            this.Password = this.connection.Password;
            NotifyOfPropertyChange(() => this.Password);

            if (settingsHost.Settings.Active != null && settingsHost.Settings.Active.Url == connection.Url)
            {
                this.IsActive = true;
                NotifyOfPropertyChange(() => this.IsActive);
            }
        }
    }
}