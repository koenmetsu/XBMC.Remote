namespace Xbmc.Core
{
    using System;

    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            // TODO: Should not be static
            this.ActiveXbmcServer = new Uri("http://fenpc100:8081/");
        }

        public Uri ActiveXbmcServer { get; set; }
    }
}