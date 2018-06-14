using System.Collections.Generic;
using System.Windows.Input;
using MeltingApp.Helpers;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using Xamarin.Forms;

namespace MeltingApp.ViewModels
{
    class ChatMainPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Message> Messages { get; }
        public Command SendCommand { get; set; } 
        string _outgoingText = string.Empty;
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private string _body;
        private int _user_Id;
        private int _utc_Timestamp;
        private SendChatQuery _sendChatQuery;




        public SendChatQuery SendChatQuery {
            get { return _sendChatQuery; }
            set {
                _sendChatQuery = value;
                OnPropertyChanged(nameof(SendChatQuery));
            }


        }

        public string OutgoingText {

            get { return _outgoingText; }
            set {

                _outgoingText = value;
                OnPropertyChanged(nameof(OutgoingText));

            }
        }

        public string Body {
            get { return _body; }
            set {
                _body = value;
                OnPropertyChanged(nameof(Body));
            }
        }

        public int User_id {
            get { return _user_Id; }
            set {
                _user_Id = value;
                OnPropertyChanged(nameof(User_id));
            }
        }

        public int Utc_Timestamp {
            get { return _utc_Timestamp; }
            set {
                _utc_Timestamp = value;
                OnPropertyChanged(nameof(Utc_Timestamp));
            }

        }

        public ChatMainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            SendCommand = new Command(HandleSendCommand);

            Messages = new ObservableRangeCollection<Message>();

            SendChatQuery = new SendChatQuery();

        }


        void HandleSendCommand()
        {

            //llamadas api
            var message = new Message
            {
                Text = OutgoingText,
                IsIncoming = false
            };



            Messages.Add(message);
            //send message
            //empty outgoing message

           // var sendChatQuery= new SendChatQuery
            //{
                 // await _apiClientService.PostAsync<SendChatQuery,SendChatQuery>
            //}

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
