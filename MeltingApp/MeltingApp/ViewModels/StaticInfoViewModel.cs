using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Validators;
using MeltingApp.Views.Pages;
using Plugin.ExternalMaps;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class StaticInfoViewModel : ViewModelBase
    { 

        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private string _responseMessage;
        private StaticInfo _staticInfoFacult;
        private StaticInfo _staticInfoUni;

        public Command OpenMapStaticFacultyCommand { get; set; }
       // public Command OpenMapStaticUniversityCommand { get; set; }
        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
            }
        }

        public StaticInfo FacultyStaticInfo
        {
            get { return _staticInfoFacult; }
            set
            {
                _staticInfoFacult = value;
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

        public StaticInfoViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            OpenMapStaticFacultyCommand = new Command(HandleOpenMapStaticFacultyCommand);
            //OpenMapStaticUniversityCommand = new Command(HandleOpenMapStaticUniversityCommand);

            FacultyStaticInfo = new StaticInfo();
            UniversityStaticInfo = new StaticInfo();

            HandleStaticInfoCommand();
        }

        async void HandleStaticInfoCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            FacultyStaticInfo = await _apiClientService.GetAsync<StaticInfo, StaticInfo>(ApiRoutes.Methods.ShowFacultyInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            }, meltingUriParser);


            var meltingUriParser2 = new MeltingUriParser();
            var userSearched = _dataBaseService.GetWithChildren<User>(u => u.id == App.LoginRequest.LoggedUserIdBackend);
            meltingUriParser2.AddParseRule(ApiRoutes.UriParameters.OnlyUniversityId, $"{userSearched.university_id}");

            UniversityStaticInfo = await _apiClientService.GetAsync<StaticInfo, StaticInfo>(ApiRoutes.Methods.ShowUniversityInfo, (success, responseMessage) =>
            {
                if (success)
                {
                    _navigationService.PushAsync<StaticInfoPage>(this);
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }
            }, meltingUriParser2);
        }

        //private async void HandleOpenMapStaticUniversityCommand()
        //{
        //    var success = await CrossExternalMaps.Current.NavigateTo("University", Double.Parse(UniversityStaticInfo.latitude.ToString()), Double.Parse(UniversityStaticInfo.longitude.ToString()));
        //    if (!success)
        //    {
        //        DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
        //    }
        //}

        private async void HandleOpenMapStaticFacultyCommand()
        {
            var success = await CrossExternalMaps.Current.NavigateTo("Faculty", Double.Parse(FacultyStaticInfo.latitude.ToString()), Double.Parse(FacultyStaticInfo.longitude.ToString()));
            if (!success)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
            }
        }
    }
}
