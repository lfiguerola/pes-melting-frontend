using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeltingApp.Interfaces;
using MeltingApp.ViewModels;
using Xamarin.Forms;

namespace MeltingApp.Services
{
    public class NavigationService : INavigationService
    {
        private Dictionary<string, Type> PagesDictionary { get; } = new Dictionary<string, Type>();

        public Page CurrentPage => NavigationPage.CurrentPage;
        public NavigationPage NavigationPage { get; set; }
        public MasterDetailPage MasterDetailPage { get; set; }

        public async Task PushAsync<TPage>(ViewModelBase viewModel = null, bool animated = true) where TPage : Page
        {
            var targetPage = GetPage(typeof(TPage).Name);
            if (viewModel != null)
            {
                (targetPage.BindingContext as ViewModelBase)?.Dispose();
                targetPage.BindingContext = viewModel;
            }
            await NavigationPage.Navigation.PushAsync(targetPage, animated);
        }

        public async Task PushModalAsync<TPage>(ViewModelBase viewModel = null, bool animated = true) where TPage : Page
        {
            var targetPaget = GetPage(typeof(TPage).Name);
            if (viewModel != null)
            {
                (targetPaget.BindingContext as ViewModelBase)?.Dispose();
                targetPaget.BindingContext = viewModel;
            }
            await NavigationPage.Navigation.PushModalAsync(targetPaget, animated);
        }

        public async Task PopAsync(bool animated = true)
        {
            await NavigationPage.Navigation.PopAsync(animated);
        }

        public async Task PopModalAsync(bool animated = true)
        {
            await NavigationPage.Navigation.PopModalAsync(animated);
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            await NavigationPage.Navigation.PopToRootAsync(animated);
        }

        public void SetRootPage<TPage>(ViewModelBase viewModel = null, bool isGestureEnabled = true) where TPage : Page
        {
            var targetPage = GetPage(typeof(TPage).Name);
            if (viewModel != null)
            {
                (targetPage.BindingContext as ViewModelBase)?.Dispose();
                targetPage.BindingContext = viewModel;
            }
            var navigationPage = new NavigationPage(targetPage);
            MasterDetailPage.Detail = navigationPage;
            MasterDetailPage.IsGestureEnabled = isGestureEnabled;

            NavigationPage = navigationPage;
        }

        Page GetPage(string pageName)
        {
            if (!PagesDictionary.ContainsKey(pageName))
            {
                throw new Exception("Unregistered page");
            }
            var pageType = PagesDictionary[pageName];
            return (Page)Activator.CreateInstance(pageType);
        }

        public void RegisterPage<TPage>() where TPage : Page
        {
            if (PagesDictionary.ContainsKey(typeof(TPage).Name))
            {
                throw new Exception("Page already registered");
            }
            PagesDictionary.Add(typeof(TPage).Name, typeof(TPage));
        }
    }
}