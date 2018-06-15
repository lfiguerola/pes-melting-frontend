using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class VoteStructure : EntityBase
    {
        private int _vote_id;
        private int _event_id;
        private int _user_id;



        public int vote_id
        {
            get { return _vote_id; }
            set
            {
                _vote_id = value;
                OnPropertyChanged(nameof(vote_id));
            }
        }
        public int event_id
        {
            get { return _event_id; }
            set
            {
                _event_id = value;
                OnPropertyChanged(nameof(event_id));
            }
        }
        public int user_id
        {
            get { return _user_id; }
            set
            {
                _user_id = value;
                OnPropertyChanged(nameof(user_id));
            }
        }

    }
}