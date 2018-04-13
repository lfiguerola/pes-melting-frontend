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
    
    public class AuthViewModel : BindableObject
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

	    public string ResponseMessage
	    {
	        get { return _responseMessage; }
	        set
	        {
	            _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
	        }
	    }

	    public AuthViewModel ()
		{
		    _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
		    _apiClientService = DependencyService.Get<IApiClientService>();
            RegisterUserCommand = new Command(HandleRegisterUserCommand);
            LoginUserCommand = new Command(HandleLoginUserCommand);
		    RegisterButtonCommand = new Command(HandleRegisterButton);
            User = new User();
        }
        
	    public Command RegisterUserCommand { get; set; }
	    public Command LoginUserCommand { get; set; }

        public Command RegisterButtonCommand { get; set; }

	    async void HandleRegisterUserCommand()
	    {
	        await _apiClientService.PostAsync<User>(User, ApiRoutes.RegisterUserMethodName, (isSuccess, responseMessage) => {
	            ResponseMessage = responseMessage;
	            DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            if (isSuccess)
	            {
	                _navigationService.SetRootPage<CodeConfirmation>();
	            }
            });
	    }

	    async void HandleLoginUserCommand()
	    {
	        await _apiClientService.PostAsync<User>(User, ApiRoutes.LoginUserMethodName, (isSuccess, responseMessage) => {
	            ResponseMessage = responseMessage;
	            DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            if (isSuccess)
	            {
	                _navigationService.SetRootPage<MainPage>();
	            }
	        });

	    }

	    private void HandleRegisterButton()
	    {
	        _navigationService.SetRootPage<RegisterPage>();
	    }

    }
}