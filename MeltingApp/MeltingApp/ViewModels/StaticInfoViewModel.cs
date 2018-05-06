using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Validators;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class StaticInfoViewModel : ViewModelBase
    { 

        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private Command _returnStaticInfoCommand;
        private StaticInfo _staticInfo;
        private string _responseMessage;

        /*public Command NavigateToStaticInfoPage
        {
            get
            {
                _returnStaticInfoCommand = new Command(HandleStaticInfoCommand);
                return _returnStaticInfoCommand;
            }
        }*/
        public Command NavigateToStaticInfoPage { get; set; }

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
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            _staticInfo = new StaticInfo();
            NavigateToStaticInfoPage = new Command(HandleStaticInfoCommand);
        }

        /*async*/
        void HandleStaticInfoCommand()
        {

            //await _apiClientService.PostAsync<User>(User, ApiRoutes.RegisterUserMethodName);
            _staticInfo = new StaticInfo()
            {
                adress = "Carrer Sparragus",
                universityName = "UPC",
                latitude = "359825.6",
                longitude = "7872.5",
                phone = "123456789"
            };
            _navigationService.SetRootPage<StaticInfoPage>(this);
        }
    }
}
