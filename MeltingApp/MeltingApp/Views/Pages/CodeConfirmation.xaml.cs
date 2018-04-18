using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeConfirmation : ContentPage
    {
        public CodeConfirmation()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}