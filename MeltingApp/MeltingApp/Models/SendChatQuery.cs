namespace MeltingApp.Models
{
    class SendChatQuery: EntityBase
    {
        private string _body;
        private int _user_Id;
        private int _utc_Timestamp;

        public string Body {
            get { return _body; }
            set {
                _body = value;
                OnPropertyChanged(nameof(Body));

            }
        }

        public int User_id
        {
            get { return _user_Id; }
            set
            {
                _user_Id = value;
                OnPropertyChanged(nameof(User_id));
            }
        }

        public int Utc_Timestamp
        {
            get { return _utc_Timestamp; }
            set
            {
                _utc_Timestamp = value;
                OnPropertyChanged(nameof(Utc_Timestamp));
            }

        }
    }
}
