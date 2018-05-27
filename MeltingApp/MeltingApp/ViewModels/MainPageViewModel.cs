using System;
using System.Collections.Generic;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;
using Plugin.ExternalMaps;

namespace MeltingApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private StaticInfo _staticInfo;
        private StaticInfo _staticInfoUni;
        private string _responseMessage;
        private User _user;
        private Event _event;
        private Event _eventSelected;
        private IEnumerable<Event> _allEvents;
        private Boolean _userAssists;
        private int _userAssistsInt;
        private Comment _comment;
        private IEnumerable<Comment> _allComments;
  
        public string Title { get; set; }

        public Command NavigateToCreateEventPageCommand { get; set; }
        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }
        public Command ViewProfileCommand { get; set; }
        public Command NavigateToStaticInfoPage { get; set; }
        public Command NavigateToGetAllEventsCommand { get; set; }
        public Command InfoEventCommand { get; set; }
        public Command NavigateToViewEventPageCommand { get; set; }
        public Command ConfirmAssistanceCommand { get; set; }
        public Command CreateCommentCommand { get; set; }
        public Command GetAllCommentsCommand { get; set; }
        public Command NavigateToFinderPage { get; set; }
        public Command OpenMapStaticFacultyCommand { get; set; }
        public Command OpenMapStaticUniversityCommand { get; set; }
        public Command OpenMapEventCommand { get; set; }
        public Command NavigateToCreateProfilePageCommand { get; set; }
        public Command NavigateToHelpPageCommand { get; set; }
        public Command NavigateToAboutPageCommand { get; set; }

        public MainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
		    NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
		    NavigateToGetAllEventsCommand = new Command(HandleNavigateToGetAllEventsCommand);
            NavigateToHelpPageCommand = new Command(HandleNavigateToHelpCommand);
            NavigateToAboutPageCommand = new Command(HandleNavigateToAboutCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            NavigateToStaticInfoPage = new Command(HandleStaticInfoCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            InfoEventCommand = new Command(HandleInfoEventCommand);
            NavigateToFinderPage = new Command(HandleFinderCommand);
            NavigateToViewEventPageCommand = new Command(HandleNavigateToViewEventPageCommand);
            ConfirmAssistanceCommand = new Command(HandleConfirmAssistanceCommand);
            OpenMapStaticFacultyCommand = new Command(HandleOpenMapStaticFacultyCommand);
            OpenMapStaticUniversityCommand = new Command(HandleOpenMapStaticUniversityCommand);
            OpenMapEventCommand = new Command(HandleOpenMapEventCommand);

            Event = new Event();
            EventSelected = new Event();
            //TODO: eliminar aquest boto
            NavigateToCreateProfilePageCommand = new Command(HandleNavigateToCreateProfilePageCommand);
            User = new User();
            CreateCommentCommand = new Command(HandleCreateCommentCommand);
            GetAllCommentsCommand = new Command(HandleGetAllCommentsCommand);
            Comment = new Comment();
            FacultyStaticInfo = new StaticInfo();
            UniversityStaticInfo = new StaticInfo();
            //StaticInfo = new StaticInfo();
            //Init();
        }

        async private void Init()
        {
            UserAssistsInt = await _apiClientService.GetAsync<int,int>(ApiRoutes.Methods.GetUserAssistance, (isSuccess, responseMessage) =>
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
            if (!UserAssists)
            {
                await _apiClientService.PostAsync<Event,Event>(Event, ApiRoutes.Methods.ConfirmAssistance,
                    (isSuccess, responseMessage) =>
                    {
                        if (isSuccess)
                        {
                            DependencyService.Get<IOperatingSystemMethods>().ShowToast("Assistance Confirmed");
                            UserAssists = true;
                        }
                        else
                        {
                            DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                        }
                    });
            }
            else
            {
                await _apiClientService.DeleteAsync<Event,Event>(ApiRoutes.Methods.UnconfirmAssistance,
                    (isSuccess, responseMessage) =>
                    {
                        if (isSuccess)
                        {
                            DependencyService.Get<IOperatingSystemMethods>().ShowToast("Assistance Unconfirmed");
                            UserAssists = false;
                        }
                        else
                        {
                            DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                        }
                    });
            }

        }

        private async void HandleOpenMapStaticUniversityCommand()
        {
            var success = await CrossExternalMaps.Current.NavigateTo("University", Double.Parse(UniversityStaticInfo.latitude.ToString()), Double.Parse(UniversityStaticInfo.longitude.ToString()));
            if (!success)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
            }
        }

        private async void HandleOpenMapEventCommand()
        {
            var success = await CrossExternalMaps.Current.NavigateTo("Location", Double.Parse(Event.latitude), Double.Parse(Event.longitude));
            if (!success)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
            }
        }

        private async void HandleOpenMapStaticFacultyCommand()
        {
            var success = await CrossExternalMaps.Current.NavigateTo("Faculty", Double.Parse(FacultyStaticInfo.latitude.ToString()), Double.Parse(FacultyStaticInfo.longitude.ToString()));
            if (!success)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
            }
        }

        async void HandleGetAllCommentsCommand()
        {
            AllComments = await _apiClientService.GetAsync<IEnumerable<Comment>,IEnumerable<Comment>>(ApiRoutes.Methods.GetAllComments, (success, responseMessage) =>
            {
                if (success)
                {
                    //_navigationService.PushAsync<ViewEvent>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
        }

        async void HandleCreateCommentCommand()
        {
            await _apiClientService.PostAsync<Comment,Comment>(Comment, ApiRoutes.Methods.CreateComment, (isSuccess, responseMessage) =>
            {
                ResponseMessage = responseMessage;
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                if (isSuccess)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Comment created successfully");
                    _navigationService.PopAsync();
                    HandleNavigateToViewEventPageCommand();
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
          
        }

        void HandleInfoEventCommand()
        {
            Event = EventSelected;
            //var eventscreated = _dataBaseService.GetWithChildren<Event>(e => true);
            //Event = AllEvents.ElementAt(id);
            _navigationService.PushAsync<ViewEvent>(this);
        }

        async void HandleNavigateToViewEventPageCommand()
        {
            Event = await _apiClientService.GetAsync<Event,Event>(ApiRoutes.Methods.ShowEvent, (success, responseMessage) =>
            {
                if (success)
                {
                    HandleGetAllCommentsCommand();
                    _navigationService.PushAsync<ViewEvent>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
        }

        async void HandleNavigateToGetAllEventsCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

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
            }, meltingUriParser);
        }

        private void HandleNavigateToCreateProfilePageCommand()
        {
            _navigationService.PushAsync<CreateProfilePage>();
        }

        void HandleNavigateToCreateEventPageCommand()
        {
            _navigationService.PushAsync<CreateEvent>();
        }

        async void HandleViewProfileCommand()
        {
            //si el perfil ja s'ha creat
            bool b = false;
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            User = await _apiClientService.GetAsync<User,User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
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
                await _navigationService.PushAsync<ProfilePage>(this);
            }
            //si no s'ha creat

        }

        async void HandleSaveEditProfileCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            await _apiClientService.PutAsync<User,User>(User, ApiRoutes.Methods.EditProfileUser, (success, responseMessage) =>
            {
                if (success)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Profile modified successfully");
                    _navigationService.PopAsync();
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            }, meltingUriParser);
        }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }


        public StaticInfo FacultyStaticInfo
        {
            get { return _staticInfo; }
            set
            {
                _staticInfo = value;
                OnPropertyChanged(nameof(FacultyStaticInfo));
            }
        }

        
        public StaticInfo UniversityStaticInfo
        {
            get { return _staticInfoUni; }
            set
            {
                _staticInfoUni = value;
                OnPropertyChanged(nameof(UniversityStaticInfo));
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


        void HandleNavigateToEditProfilePageCommand()
        {
            _navigationService.PushAsync<EditProfilePage>(this);
        }

        void HandleNavigateToHelpCommand()
        {
            
            _navigationService.PushAsync<HelpPage>(this);
        }

        void HandleNavigateToAboutCommand()
        {
            _navigationService.PushAsync<AboutPage>(this);
        }

        async void HandleStaticInfoCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            FacultyStaticInfo = await _apiClientService.GetAsync<StaticInfo,StaticInfo>(ApiRoutes.Methods.ShowFacultyInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    //DependencyService.Get<IOperatingSystemMethods>().ShowToast("Faculty StaticInfo requested");
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            }, meltingUriParser);

            UniversityStaticInfo = await _apiClientService.GetAsync<StaticInfo,StaticInfo>(ApiRoutes.Methods.ShowUniversityInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    //DependencyService.Get<IOperatingSystemMethods>().ShowToast("University StaticInfo requested");
                    _navigationService.PushAsync<StaticInfoPage>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
        }

        void HandleFinderCommand()
        {
            /*rellenar*/
            _navigationService.PushAsync<FinderPage>(this);
        }
        
    }
}