using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Plugin.Media;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private User _user;
        private University _university;
        private List<University> _universities;
        private string _responseMessage;
        private ImageSource _image1;

        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }
        public Command CreateProfileCommand { get; set; }
        public Command ViewProfileCommand { get; set; }
        public Command UploadImageCommand { get; set; }
        public Command ViewUniversitiesCommand { get; set; }
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

        public University University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChanged(nameof(University));
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

        public ImageSource Image1
        {
            get { return _image1; }
            set
            {
                _image1 = value;
                OnPropertyChanged(nameof(Image1));
            }
        }

        public ProfileViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            UploadImageCommand = new Command(HandleUploadImageCommand);
            //SetAvatarProfileCommand = new Command(HandleSetAvatarProfileCommand);
            CreateProfileCommand = new Command(HandleCreateProfileCommand);
            ViewUniversitiesCommand = new Command(HandleViewUniversitiesCommand);
            User = new User();
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

        async void HandleViewUniversitiesCommand()
        {
           
            
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

        async void HandleCreateProfileCommand()
        {

            await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.CreateProfileUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    HandleViewProfileCommand();
                   
                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                _navigationService.PopAsync(); //tornem a la main page
                _navigationService.PushAsync<ProfilePage>();
            });
        }

        void HandleNavigateToEditProfilePageCommand()
        {
            _navigationService.PushAsync<EditProfilePage>(this);
        }

        private async void HandleUploadImageCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Picking a photo is not supported");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) return;

            Image1 = ImageSource.FromStream(() => file.GetStream());
        }
    }
}
