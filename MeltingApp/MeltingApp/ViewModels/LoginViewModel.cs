using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Services;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public LoginViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            LoginCommand = new Command(HandleLoginCommand);
            RegisterPageCommand = new Command(HandleRegisterPageCommand);
        }
        public Command LoginCommand { get; set; }
        public Command RegisterPageCommand { get; set; }

        async void HandleLoginCommand()
        {
            User = new User()
            {
                username = "freyja93", email = "laufipe@gmail.com", password = "123456qaws" 
            };
            await _apiClientService.PostAsync<User>(User, ApiRoutes.RegisterUserMethodName);
            //navigationService.SetRootPage<MainPage>();
        }
        void HandleRegisterPageCommand()
        {
            _navigationService.RegisterPage<RegisterPage>();
            _navigationService.SetRootPage<RegisterPage>();
        }
    }
}
