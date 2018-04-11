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
	public class CodeConfirmationViewModel : BindableObject
    {
        INavigationService navigationService;
        public CodeConfirmationViewModel ()
		{
		    navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
		    CodeConfirmationCommand = new Command(HandleCodeConfirmationCommand);
        }

        public Command CodeConfirmationCommand { get; set; }

        void HandleCodeConfirmationCommand()
        {

            //Code for the api call

            navigationService.SetRootPage<MainPage>();
        }
    }
}