namespace MeltingApp.Models
{
    class SendChatQuery: EntityBase
    {
        private string _body;
        private string _username;
        private int _user_Id;
        private int _utc_Timestamp;
        

        public string body {
            get { return _body; }
            set {
                _body = value;
                OnPropertyChanged(nameof(body));

            }
        }

        public int user_id
        {
            get { return _user_Id; }
            set
            {
                _user_Id = value;
                OnPropertyChanged(nameof(user_id));
            }
        }

        public int utc_Timestamp
        {
            get { return _utc_Timestamp; }
            set
            {
                _utc_Timestamp = value;
                OnPropertyChanged(nameof(utc_Timestamp));
            }
        }

        public string username
        {
            get { return  _username;
         }
            set {
                 _username = value;
                OnPropertyChanged(nameof(_username));
            }

        }
    }
}
