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
        private string _responseMessage;

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

        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
            }
        }

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
            await _apiClientService.PostAsync<User>(User, ApiRoutes.ActivateUserMethodName, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    _navigationService.SetRootPage<MainPage>();
                }
            });


        }
    }
}
