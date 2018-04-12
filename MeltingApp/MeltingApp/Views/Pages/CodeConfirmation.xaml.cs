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
    public partial class CodeConfirmation : ContentPage
    {
        public CodeConfirmation()
        {
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new CodeConfirmationViewModel();
        }
    }
}