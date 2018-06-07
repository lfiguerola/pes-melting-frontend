using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MeltingApp.Interfaces;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    class ChatMainPageViewModel : ViewModelBase
    {
        public ObservableCollection<Message> Messages { get; }
        string _outgoingText = string.Empty;
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        

        public Command SendCommand { get; set; }


        public string OutgoingText {

            get { return _outgoingText; }
            set {

                _outgoingText = value;
                OnPropertyChanged(nameof(OutgoingText));

            }

        }

        public ChatMainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();

            SendCommand = new Command(HandleSendCommand);


            void HandleSendCommand() {

                //llamadas api
                var message = new Message
                {
                    Text = OutgoingText,
                    IsIncoming = false
                };

                Messages.Add(message);

            }

        }







    }
}
