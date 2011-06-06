using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    public class TvshowLadingDesignViewModel
    {
        public TvshowLadingDesignViewModel()
        {
            this.Shows = new[]
            {
            	new TvshowDesignData(),
            	new TvshowDesignData(),
            	new TvshowDesignData() { Title = "History Channel Lectures", Thumbnail = null },
            	new TvshowDesignData(),
            	new TvshowDesignData(),
            	new TvshowDesignData(),
            	new TvshowDesignData(),
            	new TvshowDesignData(),
            	new TvshowDesignData(),
            };
        }

        public IEnumerable<TvshowDesignData> Shows { get; set; }
    }
}
