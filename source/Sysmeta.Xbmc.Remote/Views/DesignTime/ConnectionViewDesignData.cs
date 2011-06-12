namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    public class ConnectionViewDesignData
    {
        public ConnectionViewDesignData()
        {
            this.MachineAddress = "localhost";
            this.Port = "8080";
            this.Username = "test";
            this.Password = "test";
        }

        public string MachineAddress { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}