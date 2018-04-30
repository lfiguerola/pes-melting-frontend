using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace MeltingApp.ViewModels
{
	public class EventViewModel : ViewModelBase
	{
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
	    private Event _event;
        private string _responseMessage;

        public Command CreateEventCommand { get; set; }
        public Command PhotoPickerCommand { get; set; }

	    public Event Event
	    {
	        get { return _event; }
	        set
	        {
	            _event = value;
	            OnPropertyChanged(nameof(User));
	        }
	    }

        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            CreateEventCommand = new Command(HandleCreateEventCommand);
            PhotoPickerCommand = new Command(HandlePhotoPickerCommand);
        }

        async void HandleCreateEventCommand()
        {
            //Falta poder fer la crida a la api quan estigui fet el endpoint
        }

        async void HandlePhotoPickerCommand()
        {
            await CrossMedia.Current.Initialize();
        }


    }
}