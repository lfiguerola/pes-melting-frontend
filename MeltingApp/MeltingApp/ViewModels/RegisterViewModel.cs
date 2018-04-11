using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeltingApp.Interfaces;
using MeltingApp.Services;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    
    public class RegisterViewModel : BindableObject
	{
	    INavigationService navigationService;
		public RegisterViewModel ()
		{
		    navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
		    RegisterCommand = new Command(HandleRegisterCommand);
        }

	    public Command RegisterCommand { get; set; }

	    void HandleRegisterCommand()
	    {
	        navigationService.SetRootPage<MainPage>();
	    }
    }
}