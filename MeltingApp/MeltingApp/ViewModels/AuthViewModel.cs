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
        public Command ResetPassCommand { get; set; }
        public Command NavigateToResetPassPageCommand { get; set; }

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
            NavigateToResetPassPageCommand = new Command(HandleNavigateToResetPassPage);
            ResetPassCommand = new Command(HandleResetPassCommand);
            User = new User();

            User.email = "laufipe@gmail.com";
            User.password = "12345678";

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
        /// <summary>
        /// guarda l'usuari a la base de dades aixi com el seu token, tambe assigna el currentLoggedUser
        /// </summary>
        /// <param name="token"></param>
        void UserRegisterInApp(Token token)
        {
            User.Token = _dataBaseService.Get<Token>(t => t.jwt.Equals(token.jwt)); //mirem si es el primer cop que fem login
            if (User.Token == null) //si es el primer cop, el fiquem a la bd
            {
                User.Token = token;
                var tokenDecoded = new JwtSecurityToken(token.jwt);
                User.id = Int32.Parse(tokenDecoded.Claims.First(c => c.Type == "sub").Value);
                _dataBaseService.UpdateWithChildren(User);
            }
            //tant si es el primer cop com si no
            _authService.SetCurrentLoggedUser(User);
            _navigationService.SetRootPage<MainPage>();
        }

        async void HandleLoginUserCommand()
        {
            bool b = false;
            var token = await _apiClientService.PostAsync<User, Token>(User, ApiRoutes.Methods.LoginUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if(!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(ResponseMessage);
                else b = true;                   
            });
            if (token != null)
            {
                UserRegisterInApp(token);
             }
            var allusers = _dataBaseService.GetCollectionWithChildren<User>(u => true);
            var alltokens = _dataBaseService.GetCollectionWithChildren<Token>(t => true);
            
            
        }
            
        async void HandleCodeConfirmationCommand()
        {
            await _apiClientService.PostAsync<User, User>(User, ApiRoutes.Methods.ActivateUser, async (isSucessActivation, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSucessActivation)
                {
                    var token = await _apiClientService.PostAsync<User, Token>(User, ApiRoutes.Methods.LoginUser, (isSuccess, responseMessage2) => {
                        ResponseMessage = responseMessage2;
                        if (!isSuccess)
                        {
                            DependencyService.Get<IOperatingSystemMethods>().ShowToast(ResponseMessage);
                        }
                    });
                    if (token != null)
                    {
                        UserRegisterInApp(token);
                    }

                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });
        }
        void HandleNavigateToLoginPage()
        {
            _navigationService.SetRootPage<LoginPage>(this);
        }

        void HandleNavigateToRegisterPage()
        {
            _navigationService.PushAsync<RegisterPage>(this);
        }

        void HandleNavigateToResetPassPage()
        {
            _navigationService.PushAsync<ResetPassPage>(this);
        }

        async void HandleResetPassCommand()
        {
            await _apiClientService.PostAsync<User, User>(User, ApiRoutes.Methods.ResetPass, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Password reseted successfully, check your email and follow the instructions!");
                    _navigationService.SetRootPage<LoginPage>();
                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });
        }


    }
}