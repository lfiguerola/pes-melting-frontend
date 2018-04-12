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
	        await _apiClientService.PostAsync<User>(User, ApiRoutes.RegisterUserMethodName, isSuccess => {
	            if(isSuccess)
	            {
	                var Message = "user registered";
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast("User has been created correctly: please confirm your email");
	                _navigationService.SetRootPage<CodeConfirmation>();
                }
	            else {
	            {
	                var Message = "something failed";
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast("User mail and/or username already exists");

                    }
                }
	        });
            
	    }
    }
}