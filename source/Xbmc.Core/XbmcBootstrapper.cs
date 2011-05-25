namespace Xbmc.Core
{
    using System;
    using System.IO.IsolatedStorage;

    using Newtonsoft.Json;

    using Telerik.Windows.Controls;

    using TinyIoC;

    using Xbmc.Core.Extensions;

    public class XbmcBootstrapper
    {
        public XbmcBootstrapper(RadPhoneApplicationFrame mainFrame)
        {
            this.RegisterNavigator(mainFrame);
            this.RegisterApplicationSettings();
        }

        private void RegisterApplicationSettings()
        {
            TinyIoCContainer.Current.Register(this.LoadSettings());
        }

        private void RegisterNavigator(RadPhoneApplicationFrame mainFrame)
        {
            var navigator = new Navigator() { MainFrame = mainFrame };
            TinyIoCContainer.Current.Register<INavigation, Navigator>(navigator);
        }

        private ApplicationSettings LoadSettings()
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists("settings.json"))
                {
                    return JsonConvert.DeserializeObject<ApplicationSettings>(FileEx.ReadAllText(storage, "settings.json"));
                }
                
                return new ApplicationSettings();
            }
        }

    }
}