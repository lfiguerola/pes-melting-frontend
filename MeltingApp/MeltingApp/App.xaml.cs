﻿using System;
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
		    InitializeDB();
            MainPage = new RootPage();
        }

	    private void InitializeDB()
	    {
	        var dataBaseService = DependencyService.Get<IDataBaseService>();
            dataBaseService.InitializeTables();
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
            DependencyService.Register<IDataBaseService, DataBaseService>();
            DependencyService.Register<IAuthService, AuthService>();
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
            navigationService.RegisterPage<CodeConfirmation>();
            navigationService.RegisterPage<RegisterPage>();
            navigationService.RegisterPage<LoginPage>();
            navigationService.RegisterPage<ProfilePage>();
            navigationService.RegisterPage<EditProfilePage>();
        }
    }
}
