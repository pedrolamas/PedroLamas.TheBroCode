using System;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PedroLamas.TheBroCode.Model;

namespace PedroLamas.TheBroCode.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            Register<INavigationService, NavigationService>();
            Register<IApplicationSettingsService, ApplicationSettingsService>();
            Register<IEmailComposeService, EmailComposeService>();
            Register<ISmsComposeService, SmsComposeService>();
            Register<IShareStatusService, ShareStatusService>();
            Register<IShareLinkService, ShareLinkService>();
            Register<IWebBrowserService, WebBrowserService>();
            Register<IMarketplaceReviewService, MarketplaceReviewService>();
            Register<IMarketplaceSearchService, MarketplaceSearchService>();

            Register<IMainModel, MainModel>();

            Register<MainViewModel>();
            Register<SettingsViewModel>();
            Register<AboutViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public AboutViewModel About
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }

        #region Helpers

        private void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TInterface>())
            {
                SimpleIoc.Default.Register<TInterface, TClass>();
            }
        }

        private void Register<TClass>() where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TClass>())
            {
                SimpleIoc.Default.Register<TClass>();
            }
        }

        private void Register<TClass>(Func<TClass> factory) where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TClass>())
            {
                SimpleIoc.Default.Register(factory);
            }
        }

        #endregion
    }
}