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
        private User _user;
        private string _responseMessage;

        readonly IValidator _validator;

        public Command NavigateToRegisterPageCommand { get; set; }
        public Command NavigateToLoginPageCommand { get; set; }
        public Command RegisterUserCommand { get; set; }
        public Command LoginUserCommand { get; set; }
        public Command CodeConfirmationCommand { get; set; }
        public Command ViewUniversitiesCommand { get; set; }
        

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
            _validator = new UserValidation();

            CodeConfirmationCommand = new Command(HandleCodeConfirmationCommand);
            RegisterUserCommand = new Command(HandleRegisterUserCommand);
            LoginUserCommand = new Command(HandleLoginUserCommand);
            NavigateToRegisterPageCommand = new Command(HandleNavigateToRegisterPage);
            NavigateToLoginPageCommand = new Command(HandleNavigateToLoginPage);
            User = new User();
        }

        async void HandleRegisterUserCommand()
        {
            //validem camps
            var validationResults = _validator.Validate(User);
            if (validationResults.IsValid)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Validation success");
                //un cop validats els camps
                await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.RegisterUser, (isSuccess, responseMessage) => {
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
            await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.LoginUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    //decodifiquem el token i posem el id al user
                    EncodeTokenAndSaveUserId();
                    _navigationService.SetRootPage<MainPage>();
                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });
            
        }


        public void EncodeTokenAndSaveUserId()
        {
            var jwtEncodedString = User.token;
            var tokenDecoded = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            User.id = Int32.Parse(tokenDecoded.Claims.First(c => c.Type == "sub").Value);
            //Console.WriteLine("sub => " + token.Claims.First(c => c.Type == "sub").Value);
        }

        async void HandleCodeConfirmationCommand()
        {
            await _apiClientService.PostAsync<User>(User, ApiRoutes.Methods.ActivateUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    _apiClientService.PostAsync(User, ApiRoutes.Methods.LoginUser);
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