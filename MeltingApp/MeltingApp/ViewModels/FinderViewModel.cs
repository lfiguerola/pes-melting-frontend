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
using Plugin.ExternalMaps;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class FinderViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private StaticInfo _staticInfo;
        private string _responseMessage;
        private string FilterToApply;
        private string _nameToFilter;
        private int _selectedFilter = -1;
        private IEnumerable<University> _allUniversities;
        private IEnumerable<Faculty> _allFaculties;
        private IEnumerable<User> _allUsernames;
        //private IEnumerable<Event> _allEvents;
        private List<FinderStructure> _allFinderStructures;
        private FinderStructure _finderStructure;
        private FinderStructure _structureSelected;
        private University _uniAux;
        private University _university;
        private User _userAux;
        private User _user;
        private Faculty _facultyAux;
        private Faculty _faculty;
        //private Event _eventAux;
        //private Event _event;
        private SearchQuery _searchquery;

        public Command ApplyFinderButtonCommand { get; set; }
        public Command infoFinderStructureCommand { get; set; }
        public Command OpenMapStaticFacultyCommand { get; set; }
        private List<string> filters = new List<string>
        {
            "Faculties",
            "Username",
            //"Events",
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
                    HandleApplyFinder();
                }
            }
        }

        public struct FinderStructure
        {
            public int absoluteId { get; set;}
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
            infoFinderStructureCommand = new Command(HandleInfoFinderStructureCommand);
            OpenMapStaticFacultyCommand = new Command(HandleOpenMapStaticFacultyCommand);
            _navigationService = DependencyService.Get<INavigationService>();
            _apiClientService = DependencyService.Get<IApiClientService>();
            _staticInfo = new StaticInfo();
            _nameToFilter = "";
            SearchQuery = new SearchQuery();
            //Event = new Event();
        }


        async void HandleApplyFinder()
        {
            int absoluteCounter = 0;
            if (FilterToApply is null)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Select a Filter first!");
            }
            else if (FilterToApply.Equals("Universities"))
            {
                //obtenim totes les universitats
                SearchQuery.query = _nameToFilter;
                AllUniversities = await _apiClientService.GetSearchAsync<SearchQuery, IEnumerable<University>>(SearchQuery, ApiRoutes.Methods.SearchUniversities, (isSuccess, responseMessage) => {
                    if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                });
                //System.Threading.Thread.Sleep(2000);

                //obtenim iterador i declarem les variables
                IEnumerator i = AllUniversities.GetEnumerator();
                _allFinderStructures = new List<FinderStructure>();


                //recorrem el IEnumerable i l'igualem al resultat
                while (i.MoveNext())
                {
                    _uniAux = (University)i.Current;
                    _finderStructure = new FinderStructure();
                    _finderStructure.absoluteId = absoluteCounter;
                    _finderStructure.resultId1 = _uniAux.location_id;
                    _finderStructure.resultId2 = _uniAux.id;
                    _finderStructure.resultName1 = _uniAux.name;
                    _finderStructure.resultName2 = _uniAux.alias;
                    _finderStructure.resultName3 = _uniAux.address;
                    _finderStructure.resultName4 = _uniAux.url;
                    _finderStructure.latitude = _uniAux.latitude;
                    _finderStructure.longitude = _uniAux.longitude;
                    _allFinderStructures.Add(_finderStructure);
                    ++absoluteCounter;
                }
                AllResults = _allFinderStructures;
            }
            else if (FilterToApply.Equals("Username"))
            {
                //obtenim tots els users
                SearchQuery.query = _nameToFilter;
                AllUsernames = await _apiClientService.GetSearchAsync<SearchQuery, IEnumerable<User>>(SearchQuery, ApiRoutes.Methods.SearchUsers, (isSuccess, responseMessage) => {
                    if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                });

                //obtenim iterador i declarem les variables
                IEnumerator i = AllUsernames.GetEnumerator();
                _allFinderStructures = new List<FinderStructure>();


                //recorrem el IEnumerable i l'igualem al resultat
                while (i.MoveNext())
                {
                    _userAux = (User)i.Current;
                    _finderStructure = new FinderStructure();
                    _finderStructure.absoluteId = absoluteCounter;
                    _finderStructure.resultId1 = _userAux.user_id;
                    _finderStructure.resultId2 = _userAux.faculty_id;
                    _finderStructure.resultId3 = _userAux.university_id;
                    _finderStructure.resultName1 = _userAux.username;
                    _finderStructure.resultName2 = _userAux.full_name;
                    _finderStructure.resultName3 = _userAux.university;
                    _finderStructure.resultName4 = _userAux.faculty;
                    _finderStructure.resultName5 = _userAux.biography;
                    _finderStructure.resultName6 = _userAux.country_code;
                    _finderStructure.resultName7 = _userAux.avatarURL;
                    _finderStructure.karma = _userAux.karma;
                    ++absoluteCounter;

                    _allFinderStructures.Add(_finderStructure);
                }
                AllResults = _allFinderStructures;
            }
            else if (FilterToApply.Equals("Faculties"))
            {
                //obtenim totes les facultats
                SearchQuery.query = _nameToFilter;
                AllFaculties = await _apiClientService.GetSearchAsync<SearchQuery, IEnumerable<Faculty>>(SearchQuery, ApiRoutes.Methods.SearchFaculties, (isSuccess, responseMessage) => {
                    if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                });

                //obtenim iterador i declarem les variables
                IEnumerator i = AllFaculties.GetEnumerator();
                _allFinderStructures = new List<FinderStructure>();


                //recorrem el IEnumerable i l'igualem al resultat
                while (i.MoveNext())
                {
                    _facultyAux = (Faculty)i.Current;
                    _finderStructure = new FinderStructure();
                    _finderStructure.absoluteId = absoluteCounter;
                    _finderStructure.resultId1 = _facultyAux.location_id;
                    _finderStructure.resultId2 = _facultyAux.id;
                    _finderStructure.resultName1 = _facultyAux.address;
                    _finderStructure.resultName2 = _facultyAux.name;
                    _finderStructure.resultName3 = _facultyAux.telephone;
                    _finderStructure.resultName4 = _facultyAux.url;
                    _finderStructure.latitude = _facultyAux.latitude;
                    _finderStructure.longitude = _facultyAux.longitude;
                    _allFinderStructures.Add(_finderStructure);
                    ++absoluteCounter;
                }
                AllResults = _allFinderStructures;
            }

           /* else if (FilterToApply.Equals("Events"))
            {
                //obtenim tots els events
                SearchQuery.query = _nameToFilter;
                AllEvents = await _apiClientService.GetSearchAsync<SearchQuery, IEnumerable<Event>>(SearchQuery, ApiRoutes.Methods.SearchEvents, (isSuccess, responseMessage) => {
                    if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                });

                //obtenim iterador i declarem les variables
                IEnumerator i = AllEvents.GetEnumerator();
                _allFinderStructures = new List<FinderStructure>();


                //recorrem el IEnumerable i l'igualem al resultat
                while (i.MoveNext())
                {
                    _eventAux = (Event)i.Current;
                    _finderStructure = new FinderStructure();
                    _finderStructure.absoluteId = absoluteCounter;
                    _finderStructure.resultId1 = _eventAux.id;
                    _finderStructure.resultId2 = _eventAux.user_id;
                    _finderStructure.resultName1 = _eventAux.title;
                    _finderStructure.resultName2 = _eventAux.description;
                    _finderStructure.resultName3 = _eventAux.address;
                    _finderStructure.resultName4 = _eventAux.date;
                    _finderStructure.resultName5 = _eventAux.name;
                    _allFinderStructures.Add(_finderStructure);
                    ++absoluteCounter;
                }
                AllResults = _allFinderStructures;
            }*/
        }

        void HandleInfoFinderStructureCommand()
        {
            int comptador = -1;
            IEnumerator i;
            /*if (FilterToApply.Equals("Events"))
            {
                i = AllEvents.GetEnumerator();
                while (i.MoveNext() && comptador != StructureSelected.absoluteId)
                {
                    ++comptador;
                    if (comptador == StructureSelected.absoluteId) Event = (Event)i.Current;
                }
                _navigationService.PushAsync<ViewEvent>(this);
            }*/
            /*else*/ if (FilterToApply.Equals("Faculties")){
                i = AllFaculties.GetEnumerator();
                while (i.MoveNext() && comptador != StructureSelected.absoluteId)
                {
                    ++comptador;
                    if (comptador == StructureSelected.absoluteId) Faculty = (Faculty)i.Current;
                }
                _navigationService.PushAsync<FacultyPage>(this);
            }
            else if (FilterToApply.Equals("Universities")){
                i = AllUniversities.GetEnumerator();
                while (i.MoveNext() && comptador != StructureSelected.absoluteId)
                {
                    ++comptador;
                    if (comptador == StructureSelected.absoluteId) University = (University)i.Current;
                }
                _navigationService.PushAsync<UniversityPage>(this);
            }
            else if (FilterToApply.Equals("Username")){
                i = AllUsernames.GetEnumerator();
                while (i.MoveNext() && comptador != StructureSelected.absoluteId)
                {
                    ++comptador;
                    if (comptador == StructureSelected.absoluteId) User = (User)i.Current;
                }
                _navigationService.PushAsync<ProfilePage>(this);
            }
        }
        private async void HandleOpenMapStaticFacultyCommand()
        {
            var success = await CrossExternalMaps.Current.NavigateTo("Faculty", Double.Parse(Faculty.latitude.ToString()), Double.Parse(Faculty.longitude.ToString()));
            if (!success)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast("Opening maps failed");
            }
        }
        /*public Event Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged(nameof(Event));
            }
        }*/
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public University University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChanged(nameof(University));
            }
        }
        public Faculty Faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChanged(nameof(Faculty));
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
       /* public IEnumerable<Event> AllEvents
        {
            get { return _allEvents; }
            set
            {
                _allEvents = value;
                OnPropertyChanged(nameof(AllEvents));
            }
        }*/
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
        public SearchQuery SearchQuery
        {
            get { return _searchquery; }
            set
            {
                _searchquery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }
        public String NameWritedToSearch
        {
            get { return _nameToFilter; }
            set
            {
                _nameToFilter = value;
                OnPropertyChanged(nameof(NameWritedToSearch));
            }
        }
        public FinderStructure StructureSelected
        {
            get { return _structureSelected; }
            set
            {
                _structureSelected = value;
                OnPropertyChanged(nameof(StructureSelected));
            }
        }
    }
}
