using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;


namespace MeltingApp.ViewModels
{
	public class EventViewModel : ViewModelBase
	{
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private string _responseMessage;

        public Command CreateEventCommand { get; set; }

        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            CreateEventCommand = new Command(HandleCreateEventCommand);
        }

        async void HandleCreateEventCommand()
        {
            //Falta poder fer la crida a la api quan estigui fet el endpoint
        }
    }
}