using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeltingApp.Interfaces;
using Xamarin.Forms;

namespace MeltingApp.Services
{
    public class NavigationService : INavigationService
    {
        Dictionary<string, Type> pagesDictionary { get; set; } = new Dictionary<string, Type>();

        public Page CurrentPage => NavigationPage.CurrentPage;
        public NavigationPage NavigationPage { get; set; }
        public MasterDetailPage MasterDetailPage { get; set; }

        public async Task PushAsync<TView>(bool animated = true)
        {
            await NavigationPage.Navigation.PushAsync(GetPage(typeof(TView).Name), animated);
        }

        public async Task PushModalAsync<TView>(bool animated = true)
        {
            await NavigationPage.Navigation.PushModalAsync(GetPage(typeof(TView).Name), animated);
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

        public void SetRootPage<TPage>(bool isGestureEnabled)
        {
            var navigationPage = new NavigationPage(GetPage(typeof(TPage).Name));
            MasterDetailPage.Detail = navigationPage;
            MasterDetailPage.IsGestureEnabled = isGestureEnabled;

            NavigationPage = navigationPage;
        }

        Page GetPage(string pageName)
        {
            if(!pagesDictionary.ContainsKey(pageName))
            {
                throw new Exception("Unregistered page");
            }
            var pageType = pagesDictionary[pageName];
            return (Page)Activator.CreateInstance(pageType);
        }

        public void RegisterPage<TPage>()
        {
            if (pagesDictionary.ContainsKey(typeof(TPage).Name))
            {
                throw new Exception("Page already registered");
            }
            pagesDictionary.Add(typeof(TPage).Name, typeof(TPage));
        }
    }
}
