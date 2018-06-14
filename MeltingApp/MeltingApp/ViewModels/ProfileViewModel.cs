using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.Views.Pages;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private User _user;
        private University _university;
        private ObservableCollection<University> _universities;
        private Faculty _faculty;
        private IEnumerable<Faculty> _faculties;
        private string _responseMessage;
        private int countriesSelectedIndex;
        private string SelectedCountry;
        private University _mySelectedUniversity;
        private Faculty _mySelectedFaculty;
        public Command NavigateToEditProfilePageCommand { get; set; }
        public Command SaveEditProfileCommand { get; set; }
        public Command CreateProfileCommand { get; set; }
        public Command ViewProfileCommand { get; set; }
        public Command ViewUniversitiesCommand { get; set; }
        public Command DeleteAccountCommand { get; set; }
        public University MySelectedUniversity
        {
            get => _mySelectedUniversity;
            set
            {
                _mySelectedUniversity = value;
                if (_mySelectedUniversity != null)
                {
                    getFaculties(_mySelectedUniversity.location_id);
                }                
            }
        }

        public Faculty MySelectedFaculty
        {
            get => _mySelectedFaculty;
            set
            {
                _mySelectedFaculty = value;
                if (_mySelectedFaculty != null)
                {
                    User.faculty_id = _mySelectedFaculty.location_id;
                }
            }

        }

        private List<string> countries = new List<string>
        {
            "AD",
            "AE",
            "AF",
            "AG",
            "AI",
            "AL",
            "AM",
            "AO",
            "AQ",
            "AR",
            "AS",
            "AT",
            "AU",
            "AW",
            "AX",
            "AZ",
            "BA",
            "BB",
            "BD",
            "BE",
            "BF",
            "BG",
            "BH",
            "BI",
            "BJ",
            "BL",
            "BM",
            "BN",
            "BO",
            "BQ",
            "BR",
            "BS",
            "BT",
            "BV",
            "BW",
            "BY",
            "BZ",
            "CA",
            "CC",
            "CD",
            "CF",
            "CG",
            "CH",
            "CI",
            "CK",
            "CL",
            "CM",
            "CN",
            "CO",
            "CR",
            "CU",
            "CV",
            "CW",
            "CX",
            "CY",
            "CZ",
            "DE",
            "DJ",
            "DK",
            "DM",
            "DO",
            "DZ",
            "EC",
            "EE",
            "EG",
            "EH",
            "ER",
            "ES",
            "ET",
            "FI",
            "FJ",
            "FK",
            "FM",
            "FO",
            "FR",
            "GA",
            "GB",
            "GD",
            "GE",
            "GF",
            "GG",
            "GH",
            "GI",
            "GL",
            "GM",
            "GN",
            "GP",
            "GQ",
            "GR",
            "GS",
            "GT",
            "GU",
            "GW",
            "GY",
            "HK",
            "HM",
            "HN",
            "HR",
            "HT",
            "HU",
            "ID",
            "IE",
            "IL",
            "IM",
            "IN",
            "IO",
            "IQ",
            "IR",
            "IS",
            "IT",
            "JE",
            "JM",
            "JO",
            "JP",
            "KE",
            "KG",
            "KH",
            "KI",
            "KM",
            "KN",
            "KP",
            "KR",
            "KW",
            "KY",
            "KZ",
            "LA",
            "LB",
            "LC",
            "LI",
            "LK",
            "LR",
            "LS",
            "LT",
            "LU",
            "LV",
            "LY",
            "MA",
            "MC",
            "MD",
            "ME",
            "MF",
            "MG",
            "MH",
            "MK",
            "ML",
            "MM",
            "MN",
            "MO",
            "MP",
            "MQ",
            "MR",
            "MS",
            "MT",
            "MU",
            "MV",
            "MW",
            "MX",
            "MY",
            "MZ",
            "NA",
            "NC",
            "NE",
            "NF",
            "NG",
            "NI",
            "NL",
            "NO",
            "NP",
            "NR",
            "NU",
            "NZ",
            "OM",
            "PA",
            "PE",
            "PF",
            "PG",
            "PH",
            "PK",
            "PL",
            "PM",
            "PN",
            "PR",
            "PS",
            "PT",
            "PW",
            "PY",
            "QA",
            "RE",
            "RO",
            "RS",
            "RU",
            "RW",
            "SA",
            "SB",
            "SC",
            "SD",
            "SE",
            "SG",
            "SH",
            "SI",
            "SJ",
            "SK",
            "SL",
            "SM",
            "SN",
            "SO",
            "SR",
            "SS",
            "ST",
            "SV",
            "SX",
            "SY",
            "SZ",
            "TC",
            "TD",
            "TF",
            "TG",
            "TH",
            "TJ",
            "TK",
            "TL",
            "TM",
            "TN",
            "TO",
            "TR",
            "TT",
            "TV",
            "TW",
            "TZ",
            "UA",
            "UG",
            "UM",
            "US",
            "UY",
            "UZ",
            "VA",
            "VC",
            "VE",
            "VG",
            "VI",
            "VN",
            "VU",
            "WF",
            "WS",
            "YE",
            "YT",
            "ZA",
            "ZM",
            "ZW"
        };

        public List<string> Countries => countries;
        
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ObservableCollection<University> Universities
        {
            get { return _universities; }
            set
            {
                _universities = value;
                OnPropertyChanged(nameof(Universities));
            }
        }

        public IEnumerable<Faculty> Faculties
        {
            get { return _faculties; }
            set
            {
                _faculties = value;
                OnPropertyChanged(nameof(Faculties));
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

        public University University
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChanged(nameof(University));
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

        public int CountriesSelectedIndex
        {
            get
            {
                return countriesSelectedIndex;
            }
            set
            {
                if (countriesSelectedIndex != value)
                {
                    countriesSelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(CountriesSelectedIndex));
                    SelectedCountry = Countries[countriesSelectedIndex];
                    User = new User();                   
                    User.country_code = SelectedCountry;
                }
            }
        }
       
        public ProfileViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
            SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
            ViewProfileCommand = new Command(HandleViewProfileCommand);
            CreateProfileCommand = new Command(HandleCreateProfileCommand);
            DeleteAccountCommand = new Command(HandleDeleteAccountCommand);
            User = new User();
            //Omplim desplegable de universities
            HandleViewUniversitiesCommand();
            HandleViewProfileCommand();
        }


        async void getFaculties(int location_id_uni)
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UniversityId, $"{location_id_uni}");

            Faculties = await _apiClientService.GetAsync<IEnumerable<Faculty>, IEnumerable<Faculty>>(ApiRoutes.Methods.GetFaculties, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {

                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            }, meltingUriParser);
        }

        async void HandleViewProfileCommand()
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
                        //HandleNavigateToCreateProfilePageCommand();
                }
            }, meltingUriParser);
            if (b)
            {
                await _navigationService.PushAsync<ProfilePage>(this);
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

        async void HandleViewUniversitiesCommand()
        {
            var universities = await _apiClientService.GetAsync<IEnumerable<University>, IEnumerable<University>>(ApiRoutes.Methods.GetUniversities,(success, responseMessage) =>
                {
                    if (success)
                    {

                    }
                    else
                    {
                        DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                    }
                });
            Universities = new ObservableCollection<University>(universities);
            
        }

        async void HandleSaveEditProfileCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");

            await _apiClientService.PutAsync<User, User>(User, ApiRoutes.Methods.EditProfileUser, (success, responseMessage) =>
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
             }, meltingUriParser);
        }
        
        private void HandleNavigateToCreateProfilePageCommand()
        {
            _navigationService.PushAsync<CreateProfilePage>();
        }
        
        void HandleNavigateToEditProfilePageCommand()
        {
            _navigationService.PushAsync<EditProfilePage>(this);
        }
        

        async void HandleCreateProfileCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");
            bool b = false;

            await _apiClientService.PostAsync<User,User>(User, ApiRoutes.Methods.CreateProfileUser, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    b = true;
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("User created correctly");
                    //_navigationService.PopAsync();
                    _navigationService.SetRootPage<MainPage>();
                    
                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            }, meltingUriParser);

            if (b)
            {
                //afegim usuari amb les seves coses a la bd
                //HandleViewProfileCommand();
                
            }
            
        }

        async void HandleDeleteAccountCommand()
        {
            var meltingUriParser = new MeltingUriParser();
            meltingUriParser.AddParseRule(ApiRoutes.UriParameters.UserId, $"{App.LoginRequest.LoggedUserIdBackend}");
            bool b = false;

            var alltokens = _dataBaseService.GetCollectionWithChildren<Token>(t => true);
            var userConsultatDB = _dataBaseService.GetWithChildren<User>(u => u.id == User.user_id);
            var tokenbuscat = _dataBaseService.Get<Token>(t => t.dbId.Equals(userConsultatDB.Token.dbId));

            await _apiClientService.DeleteAsync<User, User>(ApiRoutes.Methods.DeleteAccount, (isSuccess, responseMessage) => {
                ResponseMessage = responseMessage;
                if (isSuccess)
                {
                    b = true;
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast("User deleted correctly");
                    _navigationService.SetRootPage<LoginPage>();

                }
                else DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            }, meltingUriParser);

            if (b)
            {
                if (tokenbuscat != null)
                {
                    _dataBaseService.Delete<Token>(tokenbuscat, true);
                    _dataBaseService.Delete(User);
                }
                alltokens = _dataBaseService.GetCollectionWithChildren<Token>(t => true);
            }
        }


    }
}
