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

        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
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

    }
}