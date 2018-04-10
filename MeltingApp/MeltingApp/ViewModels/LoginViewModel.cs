using MeltingApp.Interfaces;
using MeltingApp.Services;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        INavigationService navigationService;
        public LoginViewModel()
        {
            navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            LoginCommand = new Command(HandleLoginCommand);
            RegisterCommand = new Command(HandleRegisterCommand);
        }
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        void HandleLoginCommand()
        {
            navigationService.SetRootPage<MainPage>();
        }

        void HandleRegisterCommand()
        {
            //Crida a resgister
        }
    }
}
