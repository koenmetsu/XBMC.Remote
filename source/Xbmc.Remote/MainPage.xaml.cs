using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Xbmc.Remote
{
    using Xbmc.Remote.ViewModels;
    using Xbmc.Remote.Views;
    using Xbmc_Movies;

    public partial class MainPage : XbmcPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = MainViewModel.Instance;
        }
    }
}