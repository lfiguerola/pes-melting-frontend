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
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new MainPageViewModel();
		}
	}
}