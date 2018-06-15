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


namespace MeltingApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyDK_llWYsPBgwEEYTlvQh81lBWhCZc_LgA" };
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private string _responseMessage;
        private User _user;
        private Event _event;
        private Faculty _faculty;
        private IEnumerable<Event> _allEvents;
        private Event _eventSelected;
        private int eventidaux;
        private IEnumerable<Comment> _allComments;
        private IEnumerable<User> _attendeesList;
        private int _attendeesNumber;
        private bool _userOwnsEvent;
        private IEnumerable<Address> _addresses;
        private TimeSpan _time;
        private DateTime _date;





        public Command NavigateMyFacultyInformationCommand { get; set; }
        public Command InfoEventCommand { get; set; }
        public Command NavigateToModifyEventCommand { get; set; }
        public Command ModifyEventCommand { get; set; }
        public Command NavigateToAttendeesListCommand { get; set; }



        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
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
        public IEnumerable<Address> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                OnPropertyChanged(nameof(Addresses));
            }
        }
        public Faculty Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChanged(nameof(Faculty));
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

        public IEnumerable<Event> AllEvents
        {
            get { return _allEvents; }
            set
            {
                _allEvents = value;
                OnPropertyChanged(nameof(AllEvents));
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
        public IEnumerable<User> AttendeesList
        {
            get { return _attendeesList; }
            set
            {
                _attendeesList = value;
                OnPropertyChanged(nameof(AttendeesList));
            }
        }
        public int AttendeesNumber
        {
            get { return _attendeesNumber; }
            set
            {
                _attendeesNumber = value;
                OnPropertyChanged(nameof(AttendeesNumber));
            }
        }
        public bool UserOwnsEvent
        {
            get { return _userOwnsEvent; }
            set
            {
                _userOwnsEvent = value;
                OnPropertyChanged(nameof(UserOwnsEvent));
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

        public MainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            NavigateMyFacultyInformationCommand = new Command(HandleNavigateMyFacultyInformationCommand);
            InfoEventCommand = new Command(HandleInfoEventCommand);
            ModifyEventCommand = new Command(HandleModifyEventCommand);
            NavigateToModifyEventCommand = new Command(HandleNavigateToModifyEventCommand);
            NavigateToAttendeesListCommand = new Command(HandleNavigateToAttendeesListCommand);



            Event = new Event();
            User = new User();

            SaveCurrentProfile();

            GetAllEvents();
        }

        /// <summary>
        /// Enregistra a la base de dades l'usuari que esta loggejat per poder consultar informacio sobre ell a posteriori
        /// </summary>
        async void SaveCurrentProfile()
        {
            //si el perfil ja s'ha creat
            bool b = false;
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            User = await _apiClientService.GetAsync<User, User>(ApiRoutes.Methods.GetProfileUser,
                (success, responseMessage) =>
                {
                    if (success)
                    {
                        b = true;
                    }
                    else
                    {
                        //si el perfil no s'ha creat faig crida a la creació d'aquest
                        //TODO: Treure aquest toast
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                        HandleNavigateToCreateProfilePageCommand();
                    }
                }, meltingUriParser);
            if (b)
            {
                SaveProfileInDB(User);
            }
        }

        void SaveProfileInDB(User User)
        {
            var aallusers = _dataBaseService.GetCollectionWithChildren<User>(u => true);
            var userConsultatDB = _dataBaseService.GetWithChildren<User>(u => u.id == User.user_id);
            //obtenim user i el guardem a la db
            if (userConsultatDB != null)
            {
                userConsultatDB.faculty_id = User.faculty_id;
                userConsultatDB.university_id = User.university_id;
                userConsultatDB.full_name = User.full_name;
                userConsultatDB.username = User.username;
            }

            _dataBaseService.UpdateWithChildren<User>(userConsultatDB);
            var aallusers2 = _dataBaseService.GetCollectionWithChildren<User>(u => true);
        }

        void HandleNavigateToCreateProfilePageCommand()
        {
            _navigationService.SetRootPage<CreateProfilePage>();
        }

        async void HandleNavigateMyFacultyInformationCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            Faculty = await _apiClientService.GetAsync<Faculty, Faculty>(ApiRoutes.Methods.ShowFacultyInfo,
                (isSuccess, responseMessage) =>
                {
                    ResponseMessage = responseMessage;
                    if (isSuccess)
                    {
                        _navigationService.PushAsync<FacultyPage>(this);
                    }
                    else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }, meltingUriParser);
        }


        async void GetAllEvents()
        {
            AllEvents = await _apiClientService.GetAsync<IEnumerable<Event>, IEnumerable<Event>>(
                ApiRoutes.Methods.GetAllEvents, (success, responseMessage) =>
                {
                    if (success)
                    {

                    }
                    else
                    {
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                    }
                });

            saveEventsInDB(AllEvents); //guardem tots els events a la base de dades
            AllEvents = AllEvents.OrderBy(Event => Event.num_attendees);
            AllEvents = AllEvents.Take(3);

        }
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
            if (eventidaux != 0)
            {
                //consultem tots els comentaris de l'event
                GetAllComments();
                GetAttendees();
                if (Event.user_id == App.LoginRequest.LoggedUserIdBackend)
                {
                    UserOwnsEvent = true;
                }
                else
                {
                    UserOwnsEvent = false;
                }
                _navigationService.PushAsync<ViewEvent>(this);
            }
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

            for (int i = 0; i < AllComments.Count(); ++i)
            {
                if (AllComments.ElementAt(i).user_id == App.LoginRequest.LoggedUserIdBackend)
                {
                    AllComments.ElementAt(i).IsButtonVisible = true;
                }
                else
                {
                    AllComments.ElementAt(i).IsButtonVisible = false;
                }
            }
        }
        async void GetAttendees()
        {
            bool b = false;
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.EventId, $"{eventidaux}");

            AttendeesList = await _apiClientService.GetAsync<IEnumerable<User>, IEnumerable<User>>(ApiRoutes.Methods.AttendeesList, (success, responseMessage) =>
            {
                if (success)
                {
                    b = true;
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);

                }
            }, meltingUriParser);
            if (b) AttendeesNumber = AttendeesList.Count();

        }
        void HandleNavigateToModifyEventCommand()
	    {
	        _navigationService.PushAsync<ModifyEvent>(this);
        }

        async void HandleModifyEventCommand()
	    {
	        try
	        {
	            Addresses = await geocoder.GeocodeAsync(Event.name);
	        }
	        catch (Exception e)
	        {
	            DependencyService.Get<IOperatingSystemMethods>().ShowToast("Geocoder Error");
            }
	        Event.latitude = Addresses.First().Coordinates.Latitude.ToString();
	        Event.latitude = Event.latitude.Replace(",", ".");
	        Event.longitude = Addresses.First().Coordinates.Longitude.ToString();
	        Event.longitude = Event.longitude.Replace(",", ".");
	        Event.address = Addresses.First().FormattedAddress;
	        Event.date = Time + " " + Date.ToLongDateString();
            var meltingUriParser = new MeltingUriParser();
	        meltingUriParser.AddParseRule(ApiRoutes.UriParameters.EventId, $"{eventidaux}");

	        await _apiClientService.PutAsync<Event, Event>(Event, ApiRoutes.Methods.ModifyEvent, (success, responseMessage) =>
	        {
	            if (success)
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Event modified successfully");
	                _navigationService.PopAsync();
	            }
	            else
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            }
	        }, meltingUriParser);
        }
        void HandleNavigateToAttendeesListCommand()
        {
            GetAttendees();
            _navigationService.PushAsync<AttendeesListPage>(this);
        }
    }
}