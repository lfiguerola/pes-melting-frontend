using System;
using System.Runtime.Serialization;
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
        private StaticInfo _staticInfo;
        private string _responseMessage;
	    private User _user;
	    private Event _event;

        public Command NavigateToCreateEventPageCommand { get; set; }
        public Command NavigateToViewEventPageCommand { get; set; }
        public Command NavigateToEditProfilePageCommand { get; set; }
	    public Command SaveEditProfileCommand { get; set; }
	    public Command ViewProfileCommand { get; set; }
        public Command NavigateToStaticInfoPage { get; set; }
        public Command ShowEventCommand { get; set; }

        public MainPageViewModel ()
		    {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
            NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            NavigateToStaticInfoPage = new Command(HandleStaticInfoCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            NavigateToViewEventPageCommand = new Command(HandleNavigateToViewEventPageCommand);
            Event = new Event();
            User = new User();
            StaticInfo = new StaticInfo();
        }

        void HandleNavigateToCreateEventPageCommand()
        {
            _navigationService.PushAsync<CreateEvent>();
        }
	    async void HandleNavigateToViewEventPageCommand()
	    {
	        Event = await _apiClientService.GetAsync<Event>(ApiRoutes.Methods.ShowEvent, (success, responseMessage) =>
	        {
	            if (success)
	            {
	                _navigationService.PushAsync<ViewEvent>(this);
	            }
	            else
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            }
	        });
	    }

        async void HandleViewProfileCommand()
	    {
	        bool b = false;
	        User = await _apiClientService.GetAsync<User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
	        {
	            if (success)
	            {
	                b = true;
	            }
	            else
	            {
	                DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
	            }
	        });

	        if (b)
	        {
	            await _navigationService.PushAsync<ProfilePage>(this);
            }
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

        public StaticInfo StaticInfo
        {
            get { return _staticInfo; }
            set
            {
                _staticInfo = value;
                OnPropertyChanged(nameof(StaticInfo));
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

        void HandleNavigateToEditProfilePageCommand()
	    {
	        _navigationService.PushAsync<EditProfilePage>(this);
	    }

        async void HandleStaticInfoCommand()
        {
            StaticInfo = await _apiClientService.GetAsync<StaticInfo>(ApiRoutes.Methods.ShowFacultyInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("Static Info requested successfully");
                    _navigationService.PushAsync<StaticInfoPage>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            });
            /* = new StaticInfo()
             {
                 address = "Carrer Sparragus",
                 name = "UPC",
                 latitude = 41.4113891882873F,
                 longitude = 41.4113891882873F,
                 telephone = "123456789"
             };
             _navigationService.PushAsync<StaticInfoPage>(this);*/
        }
    }
}