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
	public partial class EventList : ContentPage
	{
		public EventList ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, true);
         }         

        protected override void OnAppearing()
        {
            InvalidateMeasure();
            base.OnAppearing();
        }

    }

    
}