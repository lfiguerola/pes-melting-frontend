using System;
using System.Collections.Generic;
using System.Linq;
using Geocoding;
using Geocoding.Google;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;
using Boolean = System.Boolean;


namespace MeltingApp.ViewModels
{
	public class EventViewModel : ViewModelBase
	{
	    private IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyDK_llWYsPBgwEEYTlvQh81lBWhCZc_LgA" };
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
	    private Event _event;
	    private Boolean _userAssists;
	    private int _userAssistsInt;
	    private TimeSpan _time;
	    private DateTime _date;
	    private DateTime _minDate;
        private string _responseMessage;
	    private IEnumerable<Address> _addresses;
        public Command CreateEventCommand { get; set; }
        public Command ConfirmAssistanceCommand { get; set; }

	    public IEnumerable<Address> Addresses
	    {
	        get { return _addresses; }
	        set
	        {
	            _addresses = value;
	            OnPropertyChanged(nameof(Addresses));
	        }
	    }

        public Event Event
	    {
	        get { return _event; }
	        set
	        {
	            _event = value;
	            OnPropertyChanged(nameof(Event));
	        }
	    }
        public TimeSpan Time
	    {
	        get { return _time; }
	        set
	        {
	            _time = value;
	            OnPropertyChanged(nameof(Time));
	        }
	    }

	    public DateTime Date
	    {
	        get { return _date; }
	        set
	        {
	            _date = value;
	            OnPropertyChanged(nameof(Date));
	        }
	    }
        public DateTime MinDate
	    {
	        get { return _minDate; }
	        set
	        {
	            _minDate = value;
	            OnPropertyChanged(nameof(MinDate));
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
	    public Boolean UserAssists
        {
	        get { return _userAssists; }
	        set
	        {
	            _userAssists = value;
	            OnPropertyChanged(nameof(UserAssists));
	        }
	    }
	    public int UserAssistsInt
	    {
	        get { return _userAssistsInt; }
	        set
	        {
	            _userAssistsInt = value;
	            OnPropertyChanged(nameof(UserAssistsInt));
	        }
	    }


        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();

            CreateEventCommand = new Command(HandleCreateEventCommand);
            ConfirmAssistanceCommand = new Command(HandleConfirmAssistanceCommand);

            //Init();


            Event = new Event();

            MinDate = DateTime.Today;
        }
	    async void HandleConfirmAssistanceCommand()
	    {
	        UserAssistsInt = await _apiClientService.GetAsync<int>(ApiRoutes.Methods.GetUserAssistance, (isSuccess, responseMessage) =>
	        {
	            if (isSuccess)
	            {
	                if (UserAssistsInt == 1) UserAssists = true;
	                else UserAssists = false;
	            }
	            else
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            }
	        });
	        if (UserAssists)
	        {
	            await _apiClientService.PostAsync<Event>(Event, ApiRoutes.Methods.ConfirmAssistance,
	                (isSuccess, responseMessage) =>
	                {

	                });
	        }
	        else
	        {
	            await _apiClientService.DeleteAsync<Event>(ApiRoutes.Methods.UnconfirmAssistance,
	                (isSuccess, responseMessage) =>
	                {

	                });
	        }
	    }
        async private void Init()
	    {
	        UserAssistsInt = await _apiClientService.GetAsync<int>(ApiRoutes.Methods.GetUserAssistance, (isSuccess, responseMessage) =>
	        {
	            if (isSuccess)
	            {
	                if (UserAssistsInt == 1) UserAssists = true;
	                else UserAssists = false;
	            }
	            else
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            }
            });
        }

	    

        async void HandleCreateEventCommand()
        {
            try
            {
                Addresses = await geocoder.GeocodeAsync(Event.name);
            }
            catch (Exception e)
            {

            }

            Event.latitude = Addresses.First().Coordinates.Latitude.ToString();
            Event.latitude = Event.latitude.Replace(",", ".");
            Event.longitude = Addresses.First().Coordinates.Longitude.ToString();
            Event.longitude = Event.longitude.Replace(",", ".");
            Event.address = Addresses.First().FormattedAddress;
            Event.date = Time + " " + Date.ToLongDateString();
            await _apiClientService.PostAsync<Event>(Event, ApiRoutes.Methods.CreateEvent, (isSuccess, responseMessage) => {
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