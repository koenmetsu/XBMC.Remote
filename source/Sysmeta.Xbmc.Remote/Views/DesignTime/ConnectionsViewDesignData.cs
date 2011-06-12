namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System.Collections.Generic;

    public class ConnectionsViewDesignData
    {
        public ConnectionsViewDesignData()
        {
            Connections = new[]
                {
                    new ConnectionViewDesignData(), new ConnectionViewDesignData(), new ConnectionViewDesignData(),
                    new ConnectionViewDesignData(), new ConnectionViewDesignData(),
                };
        }

        public IEnumerable<ConnectionViewDesignData> Connections { get; set; }
    }
}