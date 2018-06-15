using MeltingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateEvent : ContentPage
	{
		public CreateEvent ()
		{
		    //BindingContext = new EventViewModel();
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }
	}
}