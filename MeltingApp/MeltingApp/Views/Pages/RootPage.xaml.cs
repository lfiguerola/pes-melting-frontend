using System.Dynamic;
using MeltingApp.Interfaces;
using MeltingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeltingApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        INavigationService navigationService;
        public RootPage()
        {
            InitializeComponent();
            ///////BindingContext = new MainPageViewModel();
            //TODO: Move logic to ViewModel
            navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);

            NavigationPage navigationPage = null;
            //Check if user is logged in
            //IsGestureEnabled = true;
            //navigationPage = new NavigationPage(new MainPage());

            //if not
            //not allow gesture -> disable menu bar
            IsGestureEnabled = false;
            navigationPage = new NavigationPage(new LoginPage());

            Detail = navigationPage;
            navigationService.NavigationPage = navigationPage;
            navigationService.MasterDetailPage = this;
            //Menu = new Menu();
            //Master = Menu;
            //Menu.ListView.ItemSelected += OnItemSelected;
        }

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MainPageViewModel;
        //    if (item != null)
        //    {
        //        string itemTitle = item.Title;
        //        var vm = (MainPageViewModel)BindingContext;
        //        if (itemTitle == "Profile")
        //        {
        //            if (vm.ViewProfileCommand.CanExecute(null))
        //            {
        //                vm.ViewProfileCommand.Execute(null);
        //            }
        //        }
        //        else if (itemTitle == "Static Info")
        //        {
        //            if (vm.NavigateToStaticInfoPage.CanExecute(null))
        //            {
        //                vm.NavigateToStaticInfoPage.Execute(null);
        //            }
        //        }
        //        else if (itemTitle == "Create Event")
        //        {
        //            if (vm.NavigateToCreateEventPageCommand.CanExecute(null))
        //            {
        //                vm.NavigateToCreateEventPageCommand.Execute(null);
        //            }
        //        }
        //        else if (itemTitle == "All Events")
        //        {
        //            if (vm.NavigateToGetAllEventsCommand.CanExecute(null))
        //            {
        //                vm.NavigateToGetAllEventsCommand.Execute(null);
        //            }
        //        }
        //        else if (itemTitle == "View Event")
        //        {
        //            if (vm.NavigateToViewEventPageCommand.CanExecute(null))
        //            {
        //                vm.NavigateToViewEventPageCommand.Execute(null);
        //            }
        //        }
        //        else if (itemTitle == "Search")
        //        {
        //            if (vm.NavigateToFinderPage.CanExecute(null))
        //            {
        //                vm.NavigateToFinderPage.Execute(null);
        //            }
        //        }
        //        else if(itemTitle == "Help")
        //        {
        //            if (vm.NavigateToHelpPageCommand.CanExecute(null))
        //            {
        //                vm.NavigateToHelpPageCommand.Execute(null);
        //            }
        //        }
        //        else if(itemTitle == "About")
        //        {
        //            if (vm.NavigateToAboutPageCommand.CanExecute(null))
        //            {
        //                vm.NavigateToAboutPageCommand.Execute(null);
        //            }
        //        }
        //        Menu.ListView.SelectedItem = null;
        //        IsPresented = false;
        //    }
        //}
    }
}