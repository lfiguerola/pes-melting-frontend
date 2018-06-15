using System;
using MeltingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new AuthViewModel();

            AnimateLogo();
        }

        async void AnimateLogo()
        {
            await logoImage.ScaleTo(1.05, 500U, Easing.Linear);
            await logoImage.ScaleTo(0.95, 500U, Easing.Linear);
            AnimateLogo();
        }
    }
}