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
    
    public class RegisterViewModel : BindableObject
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
        public string Email { get; set; }
	    public string Password { get; set; }
	    public string Username { get; set; }

	    public string ResponseMessage
	    {
	        get { return _responseMessage; }
	        set
	        {
	            _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
	        }
	    }

	    public RegisterViewModel ()
		{
		    _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
		    _apiClientService = DependencyService.Get<IApiClientService>();
            RegisterUserCommand = new Command(HandleRegisterUserCommand);
        }

	    public Command RegisterUserCommand { get; set; }

	    async void HandleRegisterUserCommand()
	    {
	        User = new User()
	        {
	            username = Username,
	            email = Email,
	            password = Password
	        };
	        await _apiClientService.PostAsync<User>(User, ApiRoutes.RegisterUserMethodName, (isSuccess, responseMessage) => {
	            ResponseMessage = responseMessage;
	            DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            if (isSuccess)
	            {
	                _navigationService.SetRootPage<CodeConfirmation>();
	            }
            });
            
	    }
    }
}