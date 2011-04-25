using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Silverlight.Testing;

namespace Xbmc.Http.Tests
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var testPage = UnitTestSystem.CreateTestPage();
            IMobileTestPage imobileTPage = testPage as IMobileTestPage;
            BackKeyPress += (s, arg) =>
            {
                bool navigateBackSuccessfull = imobileTPage.NavigateBack();
                arg.Cancel = navigateBackSuccessfull;
            };

            (Application.Current.RootVisual as PhoneApplicationFrame).Content = testPage; 	
        }
    }
}