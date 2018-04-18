using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{

    public class AuthViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private User _user;
        private string _responseMessage;

        public Command NavigateToRegisterPageCommand { get; set; }
        public Command NavigateToLoginPageCommand { get; set; }
        public Command RegisterUserCommand { get; set; }
        public Command LoginUserCommand { get; set; }
        public Command CodeConfirmationCommand { get; set; }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
            }
        }

        public AuthViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            CodeConfirmationCommand = new Command(HandleCodeConfirmationCommand);
            RegisterUserCommand = new Command(HandleRegisterUserCommand);
            LoginUserCommand = new Command(HandleLoginUserCommand);
            NavigateToRegisterPageCommand = new Command(HandleNavigateToRegisterPage);
            NavigateToLoginPageCommand = new Command(HandleNavigateToLoginPage);
            User = new User();
        }

        async void HandleRegisterUserCommand()
        {
            await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.RegisterUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    _navigationService.SetRootPage<CodeConfirmation>(this);
                }
            });
        }

        async void HandleLoginUserCommand()
        {
            await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.LoginUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    _navigationService.SetRootPage<MainPage>();
                }
            });

        }

        async void HandleCodeConfirmationCommand()
        {
            await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.ActivateUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    _navigationService.SetRootPage<MainPage>();
                }
            });
        }
        void HandleNavigateToLoginPage()
        {
            _navigationService.SetRootPage<LoginPage>(this);
        }

        void HandleNavigateToRegisterPage()
        {
            _navigationService.SetRootPage<RegisterPage>(this);
        }
    }
}