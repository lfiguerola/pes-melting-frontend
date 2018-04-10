using MeltingApp.Interfaces;
using MeltingApp.Models;
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
        }
        public Command LoginCommand { get; set; }

        async void HandleLoginCommand()
        {
            User = new User()
            {
                email = "alex.cmillan@outlook.com", code = "1151a821"
            };
            await _apiClientService.PostAsync<User>(User, "Activate");
            //navigationService.SetRootPage<MainPage>();
        }
    }
}
