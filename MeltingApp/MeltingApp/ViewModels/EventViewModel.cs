using System;
using System.Collections.Generic;
using System.Linq;
using Geocoding;
using Geocoding.Google;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Plugin.ExternalMaps;
using Xamarin.Forms;
using Boolean = System.Boolean;


namespace MeltingApp.ViewModels
{
	public class EventViewModel : ViewModelBase
	{
	    private IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyDK_llWYsPBgwEEYTlvQh81lBWhCZc_LgA" };
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private Event _event;
        private Event _eventSelected;
        private Boolean _userAssists;
	    private int _userAssistsInt;
	    private TimeSpan _time;
	    private DateTime _date;
	    private DateTime _minDate;
        private string _responseMessage;
	    private IEnumerable<Address> _addresses;
        private Comment _comment;
        private int eventidaux;
        private IEnumerable<Comment> _allComments;
        private IEnumerable<Event> _allEvents;
        private bool first_time = true;
        public Command CreateEventCommand { get; set; }
        public Command ConfirmAssistanceCommand { get; set; }
        public Command CreateCommentCommand { get; set; }
        public Command InfoEventCommand { get; set; }
        public Command NavigateToCreateEventPageCommand { get; set; }
        public Command OpenMapEventCommand { get; set; }


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

        public Event EventSelected
        {
            get { return _eventSelected; }
            set
            {
                _eventSelected = value;
                OnPropertyChanged(nameof(EventSelected));
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

        public Comment Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public IEnumerable<Comment> AllComments
        {
            get { return _allComments; }
            set
            {
                _allComments = value;
                OnPropertyChanged(nameof(AllComments));
            }
        }

        public IEnumerable<Event> AllEvents
        {
            get { return _allEvents; }
            set
            {
                _allEvents = value;
                OnPropertyChanged(nameof(AllEvents));
            }
        }


        public EventViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            CreateEventCommand = new Command(HandleCreateEventCommand);
            ConfirmAssistanceCommand = new Command(HandleConfirmAssistanceCommand);
            CreateCommentCommand = new Command(HandleCreateCommentCommand);
            InfoEventCommand = new Command(HandleInfoEventCommand);
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
            OpenMapEventCommand = new Command(HandleOpenMapEventCommand);

            //Init();

            Comment = new Comment();
            Event = new Event();
            EventSelected = new Event();
            Event.latitude = "0";
            Event.longitude = "0";
            Event.address = "C/ Jordi Girona, 1";
            Event.name = "Infern";
            MinDate = DateTime.Today;

            // GetAllComments();
            GetAllEvents();
        }

        async void GetAllEvents()
        {
            AllEvents = await _apiClientService.GetAsync<IEnumerable<Event>, IEnumerable<Event>>(ApiRoutes.Methods.GetAllEvents, (success, responseMessage) =>
            {
                if (success)
                {
                    _navigationService.PushAsync<EventList>(this);
                    
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });

            saveEventsInDB(AllEvents); //guardem tots els events a la base de dades
        }

        /// <summary>
        /// guardem tots els events a la base de dades
        /// </summary>
        /// <param name="AllEvents"></param>
        void saveEventsInDB(IEnumerable<Event> AllEvents)
        {
            var allevents_before = _dataBaseService.GetCollectionWithChildren<Event>(e => true);
            for (int i = 0; i < AllEvents.Count(); i++)
            {
                //comprovar si el event ja esta a la bd
                var eventt = AllEvents.ElementAt(i);
                bool b = false;
                for (int j = 0; j < allevents_before.Count() && !b; j++)
                {
                    if (allevents_before.ElementAt(j).id == eventt.id)
                    {
                        b = true;
                    }
                }
                if (!b)
                {
                    //si levent no esta a la bd
                    var eventToSave = AllEvents.ElementAt(i);
                    _dataBaseService.UpdateWithChildren<Event>(eventToSave);
                }

            }
            var allevents_after = _dataBaseService.GetCollectionWithChildren<Event>(e => true);
        }
        
        void HandleInfoEventCommand()
        {
            Event = EventSelected;
            eventidaux = Event.id;
            //consultem tots els comentaris de l'event
            GetAllComments();
            _navigationService.PushAsync<ViewEvent>(this);
        }
        async void HandleConfirmAssistanceCommand()
	    {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.EventId, $"{eventidaux}");

            await _apiClientService.PostAsync<Event, Event>(Event, ApiRoutes.Methods.ConfirmAssistance, (isSuccess, responseMessage) =>
	        {
	            if (isSuccess)
	            {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
	            else
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            }
	        }, meltingUriParser);
	       
	    }
        
        //async void HandleConfirmAssistanceCommand()
        //{
        //    if (!UserAssists)
        //    {
        //        await _apiClientService.PostAsync<Event, Event>(Event, ApiRoutes.Methods.ConfirmAssistance,
        //            (isSuccess, responseMessage) =>
        //            {
        //                if (isSuccess)
        //                {
        //                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Assistance Confirmed");
        //                    UserAssists = true;
        //                }
        //                else
        //                {
        //                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
        //                }
        //            });
        //    }
        //    else
        //    {
        //        await _apiClientService.DeleteAsync<Event, Event>(ApiRoutes.Methods.UnconfirmAssistance,
        //            (isSuccess, responseMessage) =>
        //            {
        //                if (isSuccess)
        //                {
        //                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Assistance Unconfirmed");
        //                    UserAssists = false;
        //                }
        //                else
        //                {
        //                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
        //                }
        //            });
        //    }

        //}
        async private void Init()
	    {
	        UserAssistsInt = await _apiClientService.GetAsync<int,int>(ApiRoutes.Methods.GetUsersAssistance, (isSuccess, responseMessage) =>
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

        async void GetAllComments()
        {
            int idde = Event.id;
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.EventId, $"{eventidaux}");

            AllComments = await _apiClientService.GetAsync<IEnumerable<Comment>, IEnumerable<Comment>>(ApiRoutes.Methods.GetEventComments, (success, responseMessage) =>
            {
                if (success)
                {
                    
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            }, meltingUriParser);
        }

        async void HandleCreateCommentCommand()
        {
            bool b = false;
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.EventId, $"{eventidaux}");

            await _apiClientService.PostAsync<Comment, Comment>(Comment, ApiRoutes.Methods.CreateComment, (isSuccess, responseMessage) =>
            {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    b = true;
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Comment created successfully");
                    _navigationService.PopAsync();
                    HandleInfoEventCommand();
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            }, meltingUriParser);

            if (b)
            {
                _dataBaseService.UpdateWithChildren<Comment>(Comment);
            }

            var allcomments = _dataBaseService.GetCollection<Comment>(c => true);
        }

        void HandleNavigateToCreateEventPageCommand()
        {
            _navigationService.PushAsync<CreateEvent>(this);
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
            var events_before = _dataBaseService.GetCollectionWithChildren<Event>(e => true);
            var resultEvent = await _apiClientService.PostAsync<Event,Event>(Event, ApiRoutes.Methods.CreateEvent, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Event created successfully");
                    _navigationService.PopAsync();
                    _navigationService.PopAsync();
                    GetAllEvents();

                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
            
            if (resultEvent != null)
            {
                _dataBaseService.UpdateWithChildren<Event>(resultEvent);
                
            }
            var events_after = _dataBaseService.GetCollectionWithChildren<Event>(e => true);
        }

        private async void HandleOpenMapEventCommand()
        {
            var success = await CrossExternalMaps.Current.NavigateTo("Location", Double.Parse(Event.latitude), Double.Parse(Event.longitude));
            if (!success)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
            }
        }
    }
}