using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private SearchQuery filter;
        private StaticInfo _staticInfo;
        private Event _event;
        private string _responseMessage;
        private string FilterToApply;
        private string _nameToFilter;
        private int _selectedFilter = -1;
        private IEnumerable<University> _allUniversities;
        private IEnumerable<Faculty> _allFaculties;
        private IEnumerable<User> _allUsernames;
        private IEnumerable<Event> _allEvents;
        private List<FinderStructure> _allFinderStructures;
        private FinderStructure _finderStructure;
        University _uniAux;

        public Command ApplyFinderButtonCommand { get; set; }

        private List<string> filters = new List<string>
        {
            "Faculties",
            "Username",
            "Events",
            "Universities"
        };

        public List<string> Filters => filters;

        public int SelectedFilterIndex
        {
            get { return _selectedFilter; }
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilterIndex));
                    FilterToApply = Filters[_selectedFilter];
                }
            }
        }

        public struct FinderStructure
        {
            public int resultId1 { get; set; }
            public int resultId2 { get; set; }
            public int resultId3 { get; set; }
            public int karma { get; set; }
            public String resultName1 { get; set; }
            public String resultName2 { get; set; }
            public String resultName3 { get; set; }
            public String resultName4 { get; set; }
            public String resultName5 { get; set; }
            public String resultName6 { get; set; }
            public String resultName7 { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }

        }
        public FinderViewModel()
        {
            ApplyFinderButtonCommand = new Command(HandleApplyFinder);
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            _staticInfo = new StaticInfo();
            _event = new Event();
            filter = new SearchQuery();

        }

        async void HandleApplyFinder()
        {
            if (FilterToApply is null)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Select a Filter first!");
            }
            else if (FilterToApply.Equals("Universities"))
            {
                //obtenim totes les universitats
                AllUniversities = await _apiClientService.GetAsync<filter , IEnumerable<University>>(ApiRoutes.Methods.SearchUniversities, (success, responseMessage) =>
                {
                    if (success)
                    { DependencyService.Get<IOperatingSystemMethods>().ShowToast("Carreguem Universitats "); }
                    else
                    { DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage); }
                });

                //obtenim iterador i declarem les variables
                IEnumerator i = AllUniversities.GetEnumerator();
                _allFinderStructures = new List<FinderStructure>();


                //recorrem el IEnumerable i l'igualem al resultat
                while (i.MoveNext())
                {
                    _uniAux = (University)i.Current;
                    _finderStructure = new FinderStructure();
                    _finderStructure.resultId1 = _uniAux.id;
                    _finderStructure.resultId2 = _uniAux.location_id;
                    _finderStructure.resultName1 = _uniAux.name;
                    _finderStructure.resultName2 = _uniAux.alias;
                    _finderStructure.resultName3 = _uniAux.address;
                    _finderStructure.resultName4 = _uniAux.url;
                    _finderStructure.latitude = _uniAux.latitude;
                    _finderStructure.longitude = _uniAux.longitude;
                    _allFinderStructures.Add(_finderStructure);
                }
                AllResults = _allFinderStructures;
            }
            else if (FilterToApply.Equals("Username"))
            {
                AllUsernames = null;
            }
            else if (FilterToApply.Equals("Faculties"))
            {
                AllFaculties = null;
            }

            else if (FilterToApply.Equals("Events"))
            {
                AllEvents = null;
            }
        }
        public IEnumerable<University> AllUniversities
        {
            get { return _allUniversities; }
            set
            {
                _allUniversities = value;
                OnPropertyChanged(nameof(AllUniversities));
            }
        }
        public IEnumerable<User> AllUsernames
        {
            get { return _allUsernames; }
            set
            {
                _allUsernames = value;
                OnPropertyChanged(nameof(AllUsernames));
            }
        }
        public IEnumerable<Faculty> AllFaculties
        {
            get { return _allFaculties; }
            set
            {
                _allFaculties = value;
                OnPropertyChanged(nameof(AllFaculties));
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
        public FinderStructure Structure
        {
            get { return _finderStructure; }
            set
            {
                _finderStructure = value;
                OnPropertyChanged(nameof(Structure));
            }
        }
        public List<FinderStructure> AllResults
        {
            get { return _allFinderStructures; }
            set
            {
                _allFinderStructures = value;
                OnPropertyChanged(nameof(AllResults));
            }
        }
    }
}
