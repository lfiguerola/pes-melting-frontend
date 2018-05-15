using System;
using MeltingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;

namespace MeltingApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
	{
		public EditProfilePage ()
		{
			InitializeComponent ();
		    NavigationPage.SetHasNavigationBar(this, false);
		    BindingContext = new ProfileViewModel();
        }

	    /*private async void UploadImageButton_Clicked(object sender, EventArgs e)
	    {
	        if (!CrossMedia.Current.IsPickPhotoSupported)
	        {
	            await DisplayAlert("No upload", "Picking a photo is not supported", "OK");
	            return;
	        }

	        var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) return;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());
	    }*/
	}
}