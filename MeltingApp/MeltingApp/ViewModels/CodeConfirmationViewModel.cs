using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Services;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class CodeConfirmationViewModel : BindableObject
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

        public string Code { get; set; }
        public string Email { get; set; }

        public CodeConfirmationViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            CodeConfirmationCommand = new Command(HandleCodeConfirmationCommand);
        }

        public Command CodeConfirmationCommand { get; set; }

        async void HandleCodeConfirmationCommand()
        {
            User = new User()
            {
                email = Email,
                code = Code
            };
            await _apiClientService.PostAsync<User>(User, ApiRoutes.ActivateUserMethodName, isSuccess => {
                if (isSuccess)
                {
                    var Message = "user registered";
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("User registered successfully");
                    _navigationService.SetRootPage<MainPage>();
                }
                else
                {
                    {
                        var Message = "something failed";
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast("Mail and/or code is not correct");

                    }
                }
            });
            //Code for the api call

            
        }
    }
}
