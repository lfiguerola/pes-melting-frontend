using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifyEvent : ContentPage
	{
		public ModifyEvent ()
		{
			InitializeComponent ();
		    NavigationPage.SetHasBackButton(this, true);
        }
    }
}