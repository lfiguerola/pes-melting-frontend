using System;
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
	    private Event _event;
	    private TimeSpan _time;
	    private DateTime _date;
	    private DateTime _minDate;
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

        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();

            CreateEventCommand = new Command(HandleCreateEventCommand);
            Event = new Event();
            Event.latitude = "0";
            Event.longitude = "0";
            Event.address = "C/ Jordi Girona, 1";
            Event.name = "Infern";

            MinDate = DateTime.Today;
        }

        async void HandleCreateEventCommand()
        {
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