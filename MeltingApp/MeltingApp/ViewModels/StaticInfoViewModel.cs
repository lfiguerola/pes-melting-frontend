using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Services;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class StaticInfoViewModel : ViewModelBase
    { 

        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private StaticInfo _staticInfo;
        private string _responseMessage;

        public StaticInfo StaticInfo
        {
            get { return _staticInfo; }
            set
            {
                _staticInfo = value;
                OnPropertyChanged(nameof(StaticInfo));
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

        public StaticInfoViewModel()
        {
            //_navigationService = DependencyService.Get<INavigationService>();
            //_apiClientService = DependencyService.Get<IApiClientService>();
            StaticInfo = new StaticInfo();
            StaticInfoCommand = new Command(HandleStaticInfoCommand);
        }
        public Command StaticInfoCommand { get; set; }

        /*async*/ void HandleStaticInfoCommand()
        {
            StaticInfo = new StaticInfo()
            {
                adress = "Carrer Sparragus", UniversityName = "UPC", latitude = "359825.6", longitude = "7872.5", phone = "123456789"
            };
            //await _apiClientService.PostAsync<User>(User, ApiRoutes.RegisterUserMethodName);
        }
    }
}
