using System;
using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;


namespace MeltingApp.ViewModels
{
<<<<<<< HEAD
    public class EventViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private Event _event;
        private string _responseMessage;
        public Command ShowEventCommand{ get; set; }

        public Event Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged(nameof(Event));
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
=======
	public class EventViewModel : ViewModelBase
	{
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
	    private Event _event;
        private string _responseMessage;

        public Command CreateEventCommand { get; set; }

        public Event Event
	    {
	        get { return _event; }
	        set
	        {
	            _event = value;
	            OnPropertyChanged(nameof(Event));
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
>>>>>>> 6c4ae03ba408e0a84184ded7cf995eba8ca70aad

        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
<<<<<<< HEAD
            ShowEventCommand = new Command(HandleShowEventCommand);

        }

        async void HandleShowEventCommand()
        {
            Event = await _apiClientService.GetAsync<Event>(ApiRoutes.Methods.ShowEvent, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    //decodifiquem el token i posem el id al user
                    _navigationService.SetRootPage<MainPage>();
                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });

        }

=======
            CreateEventCommand = new Command(HandleCreateEventCommand);
            Event = new Event();
            Event.latitude = "0";
            Event.longitude = "0";
            Event.address = "C/ Jordi Girona, 1";
            Event.name = "Infern";
            Event.date = "La fi del mon";
        }

        async void HandleCreateEventCommand()
        {
            await _apiClientService.PostAsync<Event>(Event, ApiRoutes.Methods.CreateEvent, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    _navigationService.SetRootPage<MainPage>();
                }
            });
        }
>>>>>>> 6c4ae03ba408e0a84184ded7cf995eba8ca70aad
    }
}