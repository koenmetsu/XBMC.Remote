using Microsoft.Phone.Controls;

namespace Sysmeta.Xbmc.Remote.Views.Movies
{
    using Telerik.Windows.Data;

    public partial class MovieTitleListView : PhoneApplicationPage
    {
        public MovieTitleListView()
        {
            InitializeComponent();

            this.ListBox.GroupDescriptors.Add(new PropertyGroupDescriptor("GroupOn"));
        }
    }
}