using System.Collections.Generic;
using System.Linq;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private string _responseMessage;
        private User _user;
        private Event _event;
        private Faculty _faculty;
        private IEnumerable<Event> _allEvents;
        private Event _eventSelected;



        public Command NavigateMyFacultyInformationCommand { get; set; }

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
    }
}