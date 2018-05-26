using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using FluentValidation;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Validators;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{

    public class AuthViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private IAuthService _authService;
        private User _user;
        private string _responseMessage;

        readonly IValidator _validator;

        public Command NavigateToRegisterPageCommand { get; set; }
        public Command NavigateToLoginPageCommand { get; set; }
        public Command RegisterUserCommand { get; set; }
        public Command LoginUserCommand { get; set; }
        public Command CodeConfirmationCommand { get; set; }
        public Command ViewUniversitiesCommand { get; set; }
        

        //////////////////
        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }
        public Command ViewProfileCommand { get; set; }


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
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            _authService = DependencyService.Get<IAuthService>();

            _validator = new UserValidation();

            CodeConfirmationCommand = new Command(HandleCodeConfirmationCommand);
            RegisterUserCommand = new Command(HandleRegisterUserCommand);
            LoginUserCommand = new Command(HandleLoginUserCommand);
            NavigateToRegisterPageCommand = new Command(HandleNavigateToRegisterPage);
            NavigateToLoginPageCommand = new Command(HandleNavigateToLoginPage);
            User = new User();


            //////////////////////////
            
            NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
        }

        async void HandleRegisterUserCommand()
        {
            //validem camps
            var validationResults = _validator.Validate(User);
            if (validationResults.IsValid)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Validation success");
                //un cop validats els camps
                await _apiClientService.PostAsync<User,User>(User, ApiRoutes.Methods.RegisterUser, (isSuccess, responseMessage) => {
                    ResponseMessage = responseMessage;
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                    if (isSuccess)
                    {
                        _navigationService.SetRootPage<CodeConfirmation>(this);
                    }
                });
            }
            else
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(validationResults.Errors.FirstOrDefault()?.ErrorMessage);
            }
            
        }

        async void HandleLoginUserCommand()
        {
            var token = await _apiClientService.PostAsync<User, Token>(User, ApiRoutes.Methods.LoginUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    _navigationService.SetRootPage<MainPage>();
                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });
            if (token != null)
            {
                DecodeTokenAndSaveUserId(token);
            }
            
        }

        public void DecodeTokenAndSaveUserId(Token token)
        {
            var tokenDecoded = new JwtSecurityToken(token.token);
            User.id = Int32.Parse(tokenDecoded.Claims.First(c => c.Type == "sub").Value);
            User.Token = token;
            _dataBaseService.Insert(User);
            _authService.UpdateCurrentToken(token);
            //Console.WriteLine("sub => " + token.Claims.First(c => c.Type == "sub").Value);
        }

        async void HandleCodeConfirmationCommand()
        {
            await _apiClientService.PostAsync<User, User>(User, ApiRoutes.Methods.ActivateUser, async (isSucessActivation, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSucessActivation)
                {
                    var token = await _apiClientService.PostAsync<User, Token>(User, ApiRoutes.Methods.LoginUser,
                        (isSuccessLogin, loginMessage) =>
                        {
                            if (isSuccessLogin)
                            {
                               _navigationService.SetRootPage<MainPage>();
                            }
                        });
                    DecodeTokenAndSaveUserId(token);
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


        /////////////////////
        async void HandleViewProfileCommand()
        {
            int idu = User.id;
            await _apiClientService.GetAsync<User, User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
            {
                if (success)
                {
                    _navigationService.PushAsync<ProfilePage>();
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

    }
}