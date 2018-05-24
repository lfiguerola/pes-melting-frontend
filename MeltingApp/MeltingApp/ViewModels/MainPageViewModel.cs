﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Plugin.Media;
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
	    private Event _eventSelected;
	    private ImageSource _image1;
        private IEnumerable<Event> _allEvents;

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
        public Command NavigateToFinderPage { get; set; }



        public MainPageViewModel ()
		{
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
		    NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
		    NavigateToGetAllEventsCommand = new Command(HandleNavigateToGetAllEventsCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            NavigateToStaticInfoPage = new Command(HandleStaticInfoCommand);
		    ViewProfileCommand = new Command(HandleViewProfileCommand);
		    InfoEventCommand = new Command(HandleInfoEventCommand);
		    UploadImageCommand = new Command(HandleUploadImageCommand);
            NavigateToFinderPage = new Command(HandleFinderCommand);

            NavigateToViewEventPageCommand = new Command(HandleNavigateToViewEventPageCommand);
            Event = new Event();
		    EventSelected = new Event(); 
            User = new User();
            FacultyStaticInfo = new StaticInfo();
            UniversityStaticInfo = new StaticInfo();
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

	    void HandleNavigateToCreateEventPageCommand()
        {
            _navigationService.PushAsync<CreateEvent>();
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
            get { return _staticInfo; }
            set
            {
                _staticInfo = value;
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


	    void HandleNavigateToEditProfilePageCommand()
	    {
	        _navigationService.PushAsync<EditProfilePage>(this);
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