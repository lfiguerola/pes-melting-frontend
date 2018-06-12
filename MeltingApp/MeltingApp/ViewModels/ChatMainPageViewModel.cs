using System.Collections.Generic;
using System.Windows.Input;
using MeltingApp.Helpers;
using MeltingApp.Interfaces;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    class ChatMainPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Message> Messages { get; }
        string _outgoingText = string.Empty;
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;


        public string OutgoingText {

            get { return _outgoingText; }
            set {

                _outgoingText = value;
                OnPropertyChanged(nameof(OutgoingText));

            }

        }

        public ICommand SendCommand { get; set; }
        public ChatMainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();

            SendCommand = new Command(HandleSendCommand);

            Messages = new ObservableRangeCollection<Message>();


            SendCommand = new Command(() =>
            {
                var message = new Message
                {
                    Text = OutgoingText,
                    IsIncoming = false,
                   
                };


                Messages.Add(message);

                //Send 

                OutgoingText = string.Empty;
            });


            void HandleSendCommand() {

                //llamadas api
                var message = new Message
                {
                    Text = OutgoingText,
                    IsIncoming = false
                };

                Messages.Add(message);
                //send message
                //empty outgoing message

            }

        }

      

        public void InitializeMock()
        {
            Messages.ReplaceRange(new List<Message>
            {
                    new Message { Text = "Hi Squirrel! \uD83D\uDE0A", IsIncoming = true},
                    new Message { Text = "Hi Baboon, How are you? \uD83D\uDE0A", IsIncoming = false},
                    new Message { Text = "We've a party at Mandrill's. Would you like to join? We would love to have you there! \uD83D\uDE01", IsIncoming = true, },
                    new Message { Text = "You will love it. Don't miss.", IsIncoming = true},
                    new Message { Text = "Sounds like a plan. \uD83D\uDE0E", IsIncoming = false},

                    new Message { Text = "\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49", IsIncoming = false},

            });
        }
    }
}
