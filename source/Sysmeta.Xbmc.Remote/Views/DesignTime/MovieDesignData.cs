namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System;
    using System.Windows.Media.Imaging;

    public class MovieDesignData
    {
        public MovieDesignData()
        {
            this.Year = 2002;
            this.Runtime = 117;
            this.Title = "Star Trek: Nemesis";
            this.TagLine = "A generation's final journey... begins.";
            this.Studio = "Paramount Pictures";
            this.Thumbnail = new BitmapImage() { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/f/f84ca5a8.tbn") };
            this.Director = "Stuart Baird";
            this.Plot =
                "Break out your Vulcan ears: The crew of the USS Enterprise is back. This time around, the Next Generation crew members -- including Capt. Picard, Cmdr. Riker and, of course, Data -- are dispatched as peace ambassadors to their longtime foes, the Romulans. There, they encounter a mysterious new enemy that thwarts not only the armistice process, but Earth itself!";
        }

        public int Year { get; set; }

        public BitmapImage Thumbnail { get; set; }

        public int Runtime { get; set; }

        public string Title { get; set; }

        public double Rating { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Plot { get; set; }

        public string TagLine { get; set; }

        public string Studio { get; set; }
    }
}