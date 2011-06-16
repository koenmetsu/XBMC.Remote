namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Xml;

    using Newtonsoft.Json;

    using Sysmeta.Xbmc.Remote.Model;

    public class SettingsHost
    {
        private bool isSaved = false;
        public static string SettingsFileName = "settings.json";

        private Settings appSettings;

        public SettingsHost()
        {
            this.LoadSettings();
        }

        public Settings Settings { get { return this.appSettings; } }

        public void AddConnection(Connection connection)
        {
            Connection existing = this.Settings.Connections.Where(c => c.Url == connection.Url).FirstOrDefault();

            if (existing != null)
            {
                SetActiveConnection(existing);
                return;
            }

            this.isSaved = false;
            this.appSettings.Connections.Add(connection);
            this.SetActiveConnection(connection);

            if (!this.isSaved)
            {
                this.SaveSettings();
            }
        }

        public void SetActiveConnection(Connection connection)
        {
            if (connection == null)
            {
                this.Settings.Active = null;
                this.SaveSettings();
            }
            else if (this.Settings.Active == null)
            {
                this.Settings.Active = connection;
                this.SaveSettings();
            }
            else if (this.Settings.Active.Url != connection.Url || this.Settings.Active.Username != connection.Username || this.Settings.Active.Password != connection.Password)
            {
                this.Settings.Active = connection;
                this.SaveSettings();
            }
        }

        public void RemoveConnection(Connection connection)
        {
            this.Settings.Connections.Remove(connection);
            var newConnection = this.Settings.Connections.FirstOrDefault();
            this.SetActiveConnection(newConnection);

            if (this.isSaved)
            {
                this.SaveSettings();
            }
        }

        public void Save()
        {
            this.SaveSettings();
        }

        private void LoadSettings()
        {
            using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storageFile.FileExists(SettingsFileName))
                {
                    var demo = new Connection() { Url = new Uri("http://demo:1234") };
                    this.appSettings = new Settings() {Active = demo, Connections = { demo }};
                    return;
                }

                using (IsolatedStorageFileStream fileStream = storageFile.OpenFile(SettingsFileName, FileMode.Open))
                {
                    using (var sr = new StreamReader(fileStream))
                    {
                        var text = sr.ReadToEnd();

                        this.appSettings = JsonConvert.DeserializeObject<Settings>(text);
                    }
                }

                if (this.appSettings == null)
                {
                    return;
                }
                
            }
        }

        private void SaveSettings()
        {
            if (this.appSettings == null)
            {
                return;
            }

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(SettingsFileName))
                {
                    store.DeleteFile(SettingsFileName);
                }

                using (var fs = store.OpenFile(SettingsFileName, FileMode.CreateNew))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        var text = JsonConvert.SerializeObject(this.appSettings);
                        sw.Write(text);
                        this.isSaved = true;
                    }
                }
            }
        }
    }
}