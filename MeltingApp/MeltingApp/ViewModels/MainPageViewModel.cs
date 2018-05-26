using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Plugin.ExternalMaps;

namespace MeltingApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private StaticInfo _staticInfo;
        private StaticInfo _staticInfoUni;
        private string _responseMessage;
        private User _user;
        private Event _event;
        private Event _eventSelected;
        private ImageSource _image1;
        private IEnumerable<Event> _allEvents;
        private Boolean _userAssists;
        private int _userAssistsInt;
        private Comment _comment;
        private IEnumerable<Comment> _allComments;
        private Help _help;
        private About _about;
        private HelpElment helpElment;
             

        public Command NavigateToCreateEventPageCommand { get; set; }
        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }
        public Command ViewProfileCommand { get; set; }
        public Command NavigateToStaticInfoPage { get; set; }
        public Command ShowEventCommand { get; set; }
        public Command UploadImageCommand { get; set; }
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
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
		    NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
		    NavigateToGetAllEventsCommand = new Command(HandleNavigateToGetAllEventsCommand);
            NavigateToHelpPageCommand = new Command(HandleNavigateToHelpCommand);
            NavigateToAboutPageCommand = new Command(HandleNavigateToAboutCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            NavigateToStaticInfoPage = new Command(HandleStaticInfoCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            InfoEventCommand = new Command(HandleInfoEventCommand);
            UploadImageCommand = new Command(HandleUploadImageCommand);
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
            Init();
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
            if (!UserAssists)
            {
                await _apiClientService.PostAsync<Event>(Event, ApiRoutes.Methods.ConfirmAssistance,
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
                await _apiClientService.DeleteAsync<Event>(ApiRoutes.Methods.UnconfirmAssistance,
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
            AllComments = await _apiClientService.GetAsync<IEnumerable<Comment>>(ApiRoutes.Methods.GetAllComments, (success, responseMessage) =>
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
            await _apiClientService.PostAsync<Comment>(Comment, ApiRoutes.Methods.CreateComment, (isSuccess, responseMessage) =>
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
            StaticInfo = new StaticInfo();
            HelpElment = new HelpElment();
        }

        void HandleInfoEventCommand()
        {
            Event = EventSelected;
            //Event = AllEvents.ElementAt(id);
            _navigationService.PushAsync<ViewEvent>(this);
        }

        async void HandleNavigateToViewEventPageCommand()
        {
            Event = await _apiClientService.GetAsync<Event>(ApiRoutes.Methods.ShowEvent, (success, responseMessage) =>
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
            AllEvents = await _apiClientService.GetAsync<IEnumerable<Event>>(ApiRoutes.Methods.GetAllEvents, (success, responseMessage) =>
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
            User = await _apiClientService.GetAsync<User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
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
            });

            if (b)
            {
                await _navigationService.PushAsync<ProfilePage>(this);
            }
            //si no s'ha creat

        }

        async void HandleSaveEditProfileCommand()
        {
            await _apiClientService.PutAsync<User>(User, ApiRoutes.Methods.EditProfileUser, (success, responseMessage) =>
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
            });
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

        public HelpElment HelpElment { get; }

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
            FacultyStaticInfo = await _apiClientService.GetAsync<StaticInfo>(ApiRoutes.Methods.ShowFacultyInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Faculty StaticInfo requested");
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });

            UniversityStaticInfo = await _apiClientService.GetAsync<StaticInfo>(ApiRoutes.Methods.ShowUniversityInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("University StaticInfo requested");
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

        private async void HandleUploadImageCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Picking a photo is not supported");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) return;
            Image1 = ImageSource.FromStream(() => file.GetStream());
        }
    }
}