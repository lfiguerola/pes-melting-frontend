using System.Collections.Generic;
using System.Windows.Input;
using MeltingApp.Helpers;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using MeltingApp.Resources;
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

    

        public ChatMainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            SendCommand = new Command(HandleSendCommand);

            Messages = new ObservableRangeCollection<Message>();

            SendChatQuery = new SendChatQuery();

        }


       async void  HandleSendCommand()
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
            SendChatQuery.Body = "Bamboleooo";
           // OutgoingText = string.Empty;

            SendChatQuery = await _apiClientService.PostAsync<SendChatQuery,SendChatQuery>(SendChatQuery, ApiRoutes.Methods.SendMessageChat, (isSuccess, responseMessage) =>{

                if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });
            

        }



        public void InitializeMock()
        {
            Messages.ReplaceRange(new List<Message>
            {
                    new Message { Text = "Holi! \uD83D\uDE0A", IsIncoming = true},
                    new Message { Text = "pepsicoli \uD83D\uDE0A", IsIncoming = false},
                    new Message { Text = "caracoli\uD83D\uDE01", IsIncoming = true, },
                    new Message { Text = "frijoli \uD83D\uDE48", IsIncoming = true},
                    new Message { Text = "macarroni \uD83D\uDE0E", IsIncoming = false},
                    new Message { Text = "caramboli \uD83D\uDE0E", IsIncoming = true},

                    new Message { Text = "\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49", IsIncoming = false},

            });
        }
    }
}
