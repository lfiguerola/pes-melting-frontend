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
	    private string _responseMessage;
	    private User _user;

        public Command NavigateToCreateEventPageCommand { get; set; }
	    public Command NavigateToEditProfilePageCommand { get; set; }
	    public Command SaveEditProfileCommand { get; set; }
	    public Command ViewProfileCommand { get; set; }
        public Command NavigateToCreateProfilePageCommand { get; set; }

        public MainPageViewModel ()
		{
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            NavigateToCreateEventPageCommand = new Command(HandleNavigateToCreateEventPageCommand);
		    NavigateToEditProfilePageCommand = new Command(HandleNavigateToEditProfilePageCommand);
		    SaveEditProfileCommand = new Command(HandleSaveEditProfileCommand);
		    ViewProfileCommand = new Command(HandleViewProfileCommand);
            //TODO: eliminar aquest boto
		    NavigateToCreateProfilePageCommand = new Command(HandleNavigateToCreateProfilePageCommand);
            User = new User();
        }

	    private void HandleNavigateToCreateProfilePageCommand()
	    {
	        _navigationService.PushAsync<CreateProfilePage>();
	    }

	    void HandleNavigateToCreateEventPageCommand()
        {
            _navigationService.PushAsync<CreateEvent>();
        }

	    async void HandleViewProfileCommand()
	    {
            //si el perfil ja s'ha creat
            bool b = false;
            User = await _apiClientService.GetAsync<User>(ApiRoutes.Methods.GetProfileUser, (success, responseMessage) =>
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
                    HandleNavigateToCreateProfilePageCommand();
                }
            });

            if (b)
            {
                await _navigationService.PushAsync<ProfilePage>(this);
            }
            //si no s'ha creat

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


    }
}