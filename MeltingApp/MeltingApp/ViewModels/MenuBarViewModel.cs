using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    class MenuBarViewModel : ViewModelBase
    {
        public string Title { get; set; }
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private string _responseMessage;

        public Command NavigateToProfileViewModelCommand { get; set; }
        public Command NavigateToEventViewModelCommand { get; set; }
        public Command NavigateToStaticInfoViewModelCommand { get; set; }
        public Command NavigateToFinderPage { get; set; }
        public Command NavigateToHelpPageCommand { get; set; }
        public Command NavigateToAboutPageCommand { get; set; }

        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged(nameof(ResponseMessage));
            }
        }

        public MenuBarViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            NavigateToEventViewModelCommand = new Command(HandleNavigateToEventViewModelCommand);
            NavigateToProfileViewModelCommand = new Command(HandleNavigateToProfileViewModelCommand);
            NavigateToStaticInfoViewModelCommand = new Command(HandleNavigateToStaticInfoViewModel);
            NavigateToHelpPageCommand = new Command(HandleNavigateToHelpCommand);
            NavigateToAboutPageCommand = new Command(HandleNavigateToAboutCommand);
            NavigateToFinderPage = new Command(HandleFinderCommand);
        }

        void HandleNavigateToHelpCommand()
        {
            _navigationService.PushAsync<HelpPage>(this);
        }

        void HandleNavigateToAboutCommand()
        {
            _navigationService.PushAsync<AboutPage>(this);
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
    }
}
