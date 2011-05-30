namespace Sysmeta.Xbmc.Remote.Views.DesignTime
{
    using System;
    using System.Windows.Media.Imaging;

    public class MovieDesignData
    {
        public MovieDesignData()
        {
            this.Year = 2002;
            this.Runtime = "1h 56min";
            this.Title = "Star Trek: Nemesis";
            this.Tagline = "A generation's final journey... begins.";
            this.Studio = "Paramount Pictures";
            this.Genre = "Action / Adventure / Science Fiction / Thriller";
            this.Thumbnail = new BitmapImage() { UriSource = new Uri(@"C:/Users/fen/AppData/Roaming/XBMC/userdata/Thumbnails/Video/f/f84ca5a8.tbn") };
            this.Director = "Stuart Baird";
            this.Plot = "Break out your Vulcan ears: The crew of the USS Enterprise is back. This time around, the Next Generation crew members -- including Capt. Picard, Cmdr. Riker and, of course, Data -- are dispatched as peace ambassadors to their longtime foes, the Romulans. There, they encounter a mysterious new enemy that thwarts not only the armistice process, but Earth itself!";
            this.MPAA = "PG-13";
        }

        public int Year { get; set; }

        public string MPAA { get; set; }

        public BitmapImage Thumbnail { get; set; }

        public string Runtime { get; set; }

        public string Title { get; set; }

        public double Rating { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Plot { get; set; }

        public string Tagline { get; set; }

        public string Studio { get; set; }
    }
}