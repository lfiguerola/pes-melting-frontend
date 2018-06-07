
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StaticInfoPage : ContentPage
	{
		public StaticInfoPage()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}