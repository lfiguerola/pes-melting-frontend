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
    public class FinderViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private StaticInfo _staticInfo;
        private Event _event;
        private string _responseMessage;
        private string FilterToApply;

        private int _selectedFilter;

        public Command ApplyFinderButtonCommand { get; set; }
        public int SelectedFilterIndex
        {
            get { return _selectedFilter; }
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilterIndex));
                    FilterToApply = Filters[SelectedFilterIndex];
                }
            }
        }

        private List<string> filters = new List<string>
        {
            "Faculties",
            "Users",
            "Universities",
        };

        public List<string> Filters => filters;
        public FinderViewModel()
        {
            ApplyFinderButtonCommand = new Command(HandleApplyFinder);
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            _staticInfo = new StaticInfo();
            _event = new Event();
        }

        void HandleApplyFinder()
        {
            if (FilterToApply.Equals("Universities"))
            {

            }
            else if (FilterToApply.Equals("Users"))
            {

            }
            else if (FilterToApply.Equals("Faculties"))
            {

            }
        }
    }
}
