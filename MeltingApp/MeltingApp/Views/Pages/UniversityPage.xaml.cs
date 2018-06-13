using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UniversityPage : ContentPage
	{
		public UniversityPage()
		{
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }
	}
}