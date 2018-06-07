using System;
using System.Collections.Generic;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Plugin.Media;
using Xamarin.Forms;
using Plugin.ExternalMaps;

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
        private ImageSource _image1;
        private Boolean _userAssists;
        private int _userAssistsInt;

        public Command NavigateToProfileViewModelCommand { get; set; }
        public Command UploadImageCommand { get; set; }
        public Command NavigateToEventViewModelCommand { get; set; }
        public Command NavigateToStaticInfoViewModelCommand { get; set; }
        public Command ConfirmAssistanceCommand { get; set; }
        public Command NavigateToFinderPage { get; set; }

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
            NavigateToEventViewModelCommand = new Command(HandleNavigateToEventViewModelCommand);
            NavigateToProfileViewModelCommand = new Command(HandleNavigateToProfileViewModelCommand);
            NavigateToStaticInfoViewModelCommand = new Command(HandleNavigateToStaticInfoViewModel);
            UploadImageCommand = new Command(HandleUploadImageCommand);
            NavigateToFinderPage = new Command(HandleFinderCommand);

            ConfirmAssistanceCommand = new Command(HandleConfirmAssistanceCommand);
            
            Event = new Event();
            User = new User();

            SaveCurrentProfile();
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

        void HandleNavigateToEventViewModelCommand()
        {
            EventViewModel evm = new EventViewModel();
        }

        void HandleNavigateToProfileViewModelCommand()
        {
            ProfileViewModel pvm = new ProfileViewModel();
        }

        void HandleNavigateToStaticInfoViewModel()
        {
            StaticInfoViewModel sivm = new StaticInfoViewModel();
        }

        void HandleNavigateToCreateProfilePageCommand()
        {
            _navigationService.PushAsync<CreateProfilePage>();
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