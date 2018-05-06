using System;
using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private User _user;
        private string _responseMessage;

        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }
        public Command ViewProfileCommand { get; set; }
        //public Command SetAvatarProfileCommand { get; set; }

        public ProfileViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            //SetAvatarProfileCommand = new Command(HandleSetAvatarProfileCommand);

            var savedUser = _dataBaseService.Get<User>(u => true);
            var allUsers = _dataBaseService.GetCollection<User>(user => true);
            var savedToken = _dataBaseService.Get<Token>(u => true);
            var allTokens = _dataBaseService.GetCollection<Token>(token => true);
        }


        //async void HandleSetAvatarProfileCommand()
        //{
           /* await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.AvatarProfileUser, (success, responseMessage) =>
            {
                if (success)
                {
                    _navigationService.PushAsync<ProfilePage>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });*/
        //}

        async void HandleViewProfileCommand()
        {
            await _apiClientService.GetAsync<User, User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
            {
                if (success)
                {
                    _navigationService.PushAsync<ProfilePage>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
        }

        async void HandleSaveEditProfileCommand()
        {
            await _apiClientService.PutAsync<User, User>(User, ApiRoutes.Methods.EditProfileUser, (success, responseMessage) =>
            {
                if (success)
                {
                    _navigationService.PushAsync<ProfilePage>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
        }

        void HandleNavigateToEditProfilePageCommand()
        {
            _navigationService.SetRootPage<EditProfilePage>(this);
        }

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
    }
}
