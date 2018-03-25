using MeltingApp.Interfaces;
using MeltingApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RootPage : MasterDetailPage
	{
        INavigationService navigationService;
        public RootPage ()
		{
			InitializeComponent ();
            //TODO: Move logic to ViewModel
            navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);

            NavigationPage navigationPage = null;
            //Check if user is logged in
            //IsGestureEnabled = true;
            //navigationPage = new NavigationPage(new MainPage());

            //if not
            //not allow gesture -> disable menu bar
            IsGestureEnabled = false;
            navigationPage = new NavigationPage(new LoginPage());

            Detail = navigationPage;
            navigationService.NavigationPage = navigationPage;
            navigationService.MasterDetailPage = this;
        }
    }
}