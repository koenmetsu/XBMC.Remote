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

namespace Sysmeta.Xbmc.Remote.Views.Settings
{
    using System.Windows.Data;

    public partial class ConnectionView : PhoneApplicationPage
    {
        public ConnectionView()
        {
            InitializeComponent();
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bindingExpression = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
        }
    }
}