namespace Sysmeta.Xbmc.Remote.Model
{
    using System;
    using System.Collections.Generic;

    public class Settings
    {
        public Settings()
        {
            this.Connections = new List<Connection>();
        }

        public Connection Active { get; set; }

        public List<Connection> Connections { get; set; }
    }

    public class Connection
    {
        public Uri Url { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}