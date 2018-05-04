using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowEvent : ContentPage
	{
		public ShowEvent ()
		{
			InitializeComponent ();
		    NavigationPage.SetHasNavigationBar(this, false);
		    BindingContext = new EventViewModel();
        }
	}
}