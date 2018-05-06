using MeltingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MeltingApp.ViewModels;


namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
<<<<<<< HEAD
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new MainPageViewModel();
=======
		    BindingContext = new ProfileViewModel();
>>>>>>> 6fffdbec1343be472abcfec7c058d0bacebab950
        }
	}
}