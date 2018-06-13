using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.ViewModels;
using Xamarin.Forms;

namespace MeltingApp.Interfaces
{
    public interface INavigationService
    {
        Page CurrentPage { get; }
        NavigationPage NavigationPage { get; set; }
        MasterDetailPage MasterDetailPage { get; set; }
        void SetRootPage<TPage>(ViewModelBase viewModel = null, bool isGestureEnabled = true) where TPage : Page;
        Task PushAsync<TPage>(ViewModelBase viewModel = null, bool animated = true) where TPage : Page;
        Task PopAsync(bool animated = true);
        Task PushModalAsync<TPage>(ViewModelBase viewModel = null, bool animated = true) where TPage : Page;
        Task PopModalAsync(bool animated = true);
        Task PopToRootAsync(bool animated = true);
        void RegisterPage<TPage>() where TPage : Page;
    }
}
