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
    public partial class ChatMainPage : ContentPage
    {
        ChatMainPageViewModel vm;
        int intervalInSeconds = 2;
        public ChatMainPage()
        {
            InitializeComponent();
            BindingContext = vm = new ChatMainPageViewModel();
            //  vm.InitializeMock();

            

            Device.StartTimer(TimeSpan.FromSeconds(this.intervalInSeconds), () =>
            {
                Device.BeginInvokeOnMainThread(() => vm.InitializeMock());
                vm.Messages.CollectionChanged += (sender, e) =>
                {
                    var target = vm.Messages[vm.Messages.Count - 1];
                    MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
                };

                return true;
            });


        }


     /*   void Refresh() {
            vm.InitializeMock();
            Scroll();

        }
        void Scroll()
        {
            vm.Messages.CollectionChanged += (sender, e) =>
                {
                    var target = vm.Messages[vm.Messages.Count - 1];
                    MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
                };
        }
        */
    void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;

        }
       
    }
}