using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Xbmc_Movies
{
	public class MoviesByTitleViewModel
	{
		public MoviesByTitleViewModel()
		{
			// Insert code required on object creation below this point.
		}
		
		public ObservableCollection<MovieViewModel> Movies { get; set; }
	}
}