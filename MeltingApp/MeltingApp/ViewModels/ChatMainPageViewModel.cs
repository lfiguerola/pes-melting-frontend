using System;
using System.Collections;
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
        public Command GetMessagesCommand { get; set; }
        string _outgoingText = string.Empty;
        private INavigationService _navigationService;
        private IApiClientService _apiClientService;
        private IDataBaseService _dataBaseService;
        private IAuthService _authService;
        private SendChatQuery _sendChatQuery;
        private IEnumerable<SendChatQuery> _getAllMessages;
        private TimeChat _timeChat;
        private User _user;
        private int _utctimestamp;



        public int Utctimestamp {

            get { return _utctimestamp; }
            set { _utctimestamp = value;

                OnPropertyChanged(nameof(Utctimestamp));
            }


        }


        public User user {

            get { return _user; }
            set {
                _user = value;
                OnPropertyChanged(nameof(user));
            }
        }
       

        public TimeChat TimeChat {

            get { return _timeChat; }
            set {
                _timeChat = value;
                OnPropertyChanged(nameof(TimeChat));
            }
        }
        



        public SendChatQuery SendChatQuery {
            get { return _sendChatQuery; }
            set {
                _sendChatQuery = value;
                OnPropertyChanged(nameof(SendChatQuery));
            }


        }

        public IEnumerable<SendChatQuery> GetAllMessages {
            get { return _getAllMessages; }
            set {
                _getAllMessages = value;
                OnPropertyChanged(nameof(GetAllMessages));
            }

        }

        public string OutGoingText
        {

            get { return _outgoingText; }
            set {

                _outgoingText = value;
                OnPropertyChanged(nameof(OutGoingText));

            }
        }

    

        public ChatMainPageViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>(DependencyFetchTarget.GlobalInstance);
            _apiClientService = DependencyService.Get<IApiClientService>();
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            _authService = DependencyService.Get<IAuthService>();

            SendCommand = new Command(HandleSendCommand);
            GetMessagesCommand = new Command(HandGetAllMessages);
           

            Messages = new ObservableRangeCollection<Message>();

            SendChatQuery = new SendChatQuery();
            TimeChat = new TimeChat();
            user = new User();
            Utctimestamp = 0;
            HandGetAllMessages();
          
        }


        async void HandGetAllMessages() {
            bool b = false;
            TimeChat.since = Utctimestamp;
            GetAllMessages = await _apiClientService.GetSearchAsyncMessages<TimeChat, IEnumerable<SendChatQuery>>(TimeChat,ApiRoutes.Methods.GetAllMessagesChat, (isSuccess, responseMessage) => {

                if (isSuccess) {
                    b = true;
                }
                else
                {
                    DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
                }


              });

            if (b) {
               IEnumerator i = GetAllMessages.GetEnumerator();
                user = _authService.GetCurrentLoggedUser();

                while (i.MoveNext())
                { 
                    var messageaux = (SendChatQuery)i.Current;
                    Message m = new Message();
                    m.Text = messageaux.body;
                    m.Username = messageaux.username;
                    if (messageaux.user_id.Equals( user.id))
                    {
                        m.IsIncoming = false;
                    }
                    else {

                        m.IsIncoming = true; }

                    if (Messages.Contains(m)) { }
                        else Messages.Add(m);
                    Utctimestamp = messageaux.utc_Timestamp;
                }
                

            }

        }

        async void  HandleSendCommand()
        {
            var message = new Message
            {
                Text = OutGoingText,
                IsIncoming = false
            };

            Messages.Add(message);
            
            SendChatQuery.body = OutGoingText;
    
            SendChatQuery = await _apiClientService.PostAsync<SendChatQuery,SendChatQuery>(SendChatQuery, ApiRoutes.Methods.SendMessageChat, (isSuccess, responseMessage) =>{

                if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);
            });
            OutGoingText = string.Empty;

        }

        public void InitializeMock() {

            HandGetAllMessages();
        }
    }

}
