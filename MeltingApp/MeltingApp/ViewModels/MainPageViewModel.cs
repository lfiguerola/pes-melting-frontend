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
        private ImageSource _image1;
        private Boolean _userAssists;
        private int _userAssistsInt;
        private Comment _comment;
        private IEnumerable<Comment> _allComments;
        private ImageSource _image1;
        public string Title { get; set; }

        public Command NavigateToCreateEventPageCommand { get; set; }
        public Command NavigateToProfileViewModelCommand { get; set; }
        public Command NavigateToStaticInfoPage { get; set; }
        public Command NavigateToGetAllEventsCommand { get; set; }
        public Command InfoEventCommand { get; set; }
        public Command ShowEventCommand { get; set; }
        public Command UploadImageCommand { get; set; }
        public Command NavigateToEventViewModelCommand { get; set; }
        public Command NavigateToViewEventPageCommand { get; set; }
        public Command ConfirmAssistanceCommand { get; set; }
        public Command NavigateToFinderPage { get; set; }
        public Command OpenMapStaticFacultyCommand { get; set; }
        public Command OpenMapStaticUniversityCommand { get; set; }
        public Command OpenMapEventCommand { get; set; }
        public Command NavigateToCreateProfilePageCommand { get; set; }
        public Command NavigateToHelpPageCommand { get; set; }
        public Command NavigateToAboutPageCommand { get; set; }

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

        public ImageSource Image1
        {
            get { return _image1; }
            set
            {
                _image1 = value;
                OnPropertyChanged(nameof(Image1));
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

        public MainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
            NavigateToEventViewModelCommand = new Command(HandleNavigateToEventViewModelCommand);
            NavigateToStaticInfoPage = new Command(HandleStaticInfoCommand);
            NavigateToProfileViewModelCommand = new Command(HandleNavigateToProfileViewModelCommand);
            UploadImageCommand = new Command(HandleUploadImageCommand);
            NavigateToFinderPage = new Command(HandleFinderCommand);
            ConfirmAssistanceCommand = new Command(HandleConfirmAssistanceCommand);
            OpenMapStaticFacultyCommand = new Command(HandleOpenMapStaticFacultyCommand);
            OpenMapStaticUniversityCommand = new Command(HandleOpenMapStaticUniversityCommand);
            OpenMapEventCommand = new Command(HandleOpenMapEventCommand);

            Event = new Event();
            User = new User();
            FacultyStaticInfo = new StaticInfo();
            UniversityStaticInfo = new StaticInfo();

            SaveCurrentProfile();
        }

        async void SaveCurrentProfile()
        {
            //si el perfil ja s'ha creat
            bool b = false;
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            User = await _apiClientService.GetAsync<User, User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
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
               

        void HandleNavigateToEventViewModelCommand()
        {
            EventViewModel evm = new EventViewModel();
        }

        void HandleNavigateToProfileViewModelCommand()
        {
            ProfileViewModel pvm = new ProfileViewModel();
        }

        void HandleNavigateToCreateEventPageCommand()
        {
            _navigationService.PushAsync<CreateEvent>();
        }

        void HandleNavigateToCreateProfilePageCommand()
        {
            _navigationService.PushAsync<CreateProfilePage>();
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


            var meltingUriParser2 = new MeltingUriParser();
            var userSearched = _dataBaseService.GetWithChildren<User>(u => u.id == App.LoginRequest.LoggedUserIdBackend);
            meltingUriParser2.AddParseRule(ApiRoutes.UriParameters.OnlyUniversityId, $"{userSearched.university_id}");

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
            },meltingUriParser2);
        }

        void HandleFinderCommand()
        {
            /*rellenar*/
            _navigationService.PushAsync<FinderPage>(this);
        }
        
    }
}