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
            BindingContext = new MenuBarViewModel();
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
            Menu = new Menu();
            Master = Menu;
            Menu.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuBarViewModel;
            if (item != null)
            {
                string itemTitle = item.Title;
                var vm = (MenuBarViewModel)BindingContext;
                if (itemTitle == "My Profile")
                {
                    if (vm.NavigateToProfileViewModelCommand.CanExecute(null))
                    {
                        vm.NavigateToProfileViewModelCommand.Execute(null);
                    }
                }
                else if (itemTitle == "My Faculty")
                {
                    if (vm.NavigateToStaticInfoViewModelCommand.CanExecute(null))
                    {
                        vm.NavigateToStaticInfoViewModelCommand.Execute(null);
                    }
                }
                else if (itemTitle == "Events")
                {
                    if (vm.NavigateToEventViewModelCommand.CanExecute(null))
                    {
                        vm.NavigateToEventViewModelCommand.Execute(null);
                    }
                }
                else if (itemTitle == "Chat") {
                    if (vm.NavigateToChatMainPageViewModelCommand.CanExecute(null))
                    {
                        vm.NavigateToChatMainPageViewModelCommand.Execute(null);
                    }

                }
                else if (itemTitle == "Search")
                {
                    if (vm.NavigateToFinderPage.CanExecute(null))
                    {
                        vm.NavigateToFinderPage.Execute(null);
                    }
                }
                else if (itemTitle == "Help")
                {
                    if (vm.NavigateToHelpPageCommand.CanExecute(null))
                    {
                        vm.NavigateToHelpPageCommand.Execute(null);
                    }
                }
                else if (itemTitle == "About")
                {
                    if (vm.NavigateToAboutPageCommand.CanExecute(null))
                    {
                        vm.NavigateToAboutPageCommand.Execute(null);
                    }
                }
                Menu.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}