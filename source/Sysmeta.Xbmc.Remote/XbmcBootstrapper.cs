namespace Sysmeta.Xbmc.Remote
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Controls;

    using Caliburn.Micro;

    using Microsoft.Phone.Controls;

    using Sysmeta.Xbmc.Client;
    using Sysmeta.Xbmc.Remote.Services;
    using Sysmeta.Xbmc.Remote.ViewModels;
    using Sysmeta.Xbmc.Remote.ViewModels.Movies;
    using Sysmeta.Xbmc.Remote.Views.Movies;

    public class XbmcBootstrapper : PhoneBootstrapper
    {
        private PhoneContainer container;

        public XbmcBootstrapper()
        {
            this.container = new PhoneContainer(this.RootFrame);

            container.RegisterPhoneServices();
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<MainMenuViewModel>();
            container.PerRequest<MoviesLandingViewModel>();
            container.PerRequest<MovieTitleListViewModel>();
            container.PerRequest<MovieDetailedViewModel>();

            container.RegisterSingleton(typeof(ICache), null, typeof(Cache));
            container.RegisterSingleton(typeof(IXbmcHost), null, typeof(XbmcHost));
            container.RegisterSingleton(typeof(IProgressService), null, typeof(ProgressService));


            AddCustomConventions();

            LogManager.GetLog = _ => new Logger();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, viewModelType);
                        return true;
                    }

                    return false;
                };
        }
    }

    public class Logger : ILog
    {
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(string.Format("INFO: {0}", format), args);
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(string.Format("WARN: {0}", format), args);
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine("ERROR: {0}", exception.Message);
        }
    }
}