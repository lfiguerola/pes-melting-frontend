using System;
using MeltingApp.Interfaces;
using MeltingApp.Services;
using MeltingApp.Views;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();
            AddStaticResources();
            RegisterServices();
            RegisterPages();
            MainPage = new RootPage();
        }        

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private void RegisterServices()
        {
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IApiClientService, ApiClientService>();
        }

        private void AddStaticResources()
        {
           // Resources.Add("SomeConverter", new SomeConverter());
        }

        private void RegisterPages()
        {
            var navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            navigationService.RegisterPage<RootPage>();
            navigationService.RegisterPage<MainPage>();
        }
    }
}
