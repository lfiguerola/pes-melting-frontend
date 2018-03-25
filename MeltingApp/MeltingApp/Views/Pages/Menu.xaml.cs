using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
        public static List<string> FakeMenu = new List<string>
        {
            "User",
            "Events",
            "Forum",
            "Etc.."
        };
		public Menu ()
		{
			InitializeComponent ();
		}
	}
}