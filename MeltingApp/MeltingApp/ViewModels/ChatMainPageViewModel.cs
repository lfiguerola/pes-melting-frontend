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
            
            // GetAllMessages = new IEnumerable<SendChatQuery>(); 
            // if (!isSuccess) DependencyService.Get<IOperatingSystemMethods>().ShowToast(responseMessage);

        }

        async void Refresh()
        {
            //llamada a HandGetAllMessages

        }

        async void HandGetAllMessages() {
            bool b = false;
            DateTime dateTime = DateTime.Now;
            TimeChat.since = (int)dateTime.TimeOfDay.TotalMilliseconds;
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
                //  aqui  he de volver hacer GetAllMessages = 
                // prueba();
               IEnumerator i = GetAllMessages.GetEnumerator();
                user = _authService.GetCurrentLoggedUser();
                Console.Write("Mi usuario: "+user.id);

                while (i.MoveNext())
                {

                   
                    var messageaux = (SendChatQuery)i.Current;
                    Console.Write("Mensaje usuarios: "+ messageaux.user_id);
                    Message m = new Message();
                    m.Text = messageaux.body;
                    m.Username = messageaux.username;
                    if (messageaux.user_id.Equals( user.id))
                    {
                        m.IsIncoming = true;
                    }
                    else {

                        m.IsIncoming = false; }
                
                    Messages.Add(m);



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





        public void prueba() {

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
        public void InitializeMock()
        {
            HandGetAllMessages();
           

        }
    }
}
