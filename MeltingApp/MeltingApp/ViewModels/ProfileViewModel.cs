using System;
using System.Collections.Generic;
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
        private User _user;
        private string _responseMessage;
        
        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }

        public Command ViewProfileCommand { get; set; }
        //public Command SetAvatarProfileCommand { get; set; }

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

        public ProfileViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            //SetAvatarProfileCommand = new Command(HandleSetAvatarProfileCommand);
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
            bool b = false;
            User = await _apiClientService.GetAsync<User>(ApiRoutes.Methods.GetProfileUser,
                (success, responseMessage) =>
                {
                    if (success)
                    {
                        b = true;
                    }
                    else
                    {
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                    }
                });

            if (b)
            {
                await _navigationService.PushAsync<ProfilePage>(this);
            }
        }

        async void HandleSaveEditProfileCommand()
        {
            await _apiClientService.PutAsync<User>(User, ApiRoutes.Methods.EditProfileUser,
                (success, responseMessage) =>
                {
                    if (success)
                    {
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast("Profile modified successfully");
                        _navigationService.PopAsync();
                    }
                    else
                    {
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                    }
                });
        }

        void HandleNavigateToEditProfilePageCommand()
        {
            _navigationService.PushAsync<EditProfilePage>(this);
        }
    }
}
