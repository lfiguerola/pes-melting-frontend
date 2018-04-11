using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeltingApp.Interfaces
{
    public interface INavigationService
    {
        Page CurrentPage { get; }
        NavigationPage NavigationPage { get; set; }
        MasterDetailPage MasterDetailPage { get; set; }
        void SetRootPage<TPage>(bool isGestureEnabled = true);
        Task PushAsync<TPage>(bool animated = true);
        Task PopAsync(bool animated = true);
        Task PushModalAsync<TPage>(bool animated = true);
        Task PopModalAsync(bool animated = true);
        Task PopToRootAsync(bool animated = true);
        void RegisterPage<TPage>();
    }
}
