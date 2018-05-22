using System.Collections.Generic;
using System.Runtime.Serialization;
using Java.Lang;
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
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
	    private Event _event;
	    private Boolean _userAssists;
	    private int _userAssistsInt;
        private string _responseMessage;
        public Command CreateEventCommand { get; set; }
        public Command ConfirmAssistanceCommand { get; set; }

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

            Init();

            

            Event = new Event();
            Event.latitude = "0";
            Event.longitude = "0";
            Event.address = "C/ Jordi Girona, 1";
            Event.name = "Infern";
            Event.date = "La fi del mon";
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

	    async void HandleConfirmAssistanceCommand()
	    {
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
    }
}