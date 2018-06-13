using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FacultyPage : ContentPage
	{
		public FacultyPage()
		{
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }
	}
}