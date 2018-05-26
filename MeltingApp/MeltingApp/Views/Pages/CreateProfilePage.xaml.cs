using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateProfilePage : ContentPage
	{
		public CreateProfilePage ()
		{
			InitializeComponent ();
		    BindingContext = new ProfileViewModel();
        }
	}
}