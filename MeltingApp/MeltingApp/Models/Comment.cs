using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class Comment : EntityBase
    {
        private int _commentId;
        private string _content;
        private string _updatedAt;
        private string _createdAt;
        private int _eventId;
        private int _userId;


        public int comment_id
        {
            get { return _commentId;}
            set
            {
                _commentId = value;
                OnPropertyChanged(nameof(comment_id));
            }
        }
        public string content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(content));
            }
        }

        public string created_at
        {
            get { return _createdAt; }
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(created_at));
            }
        }

        public string updated_at
        {
            get { return _updatedAt; }
            set
            {
                _updatedAt = value;
                OnPropertyChanged(nameof(updated_at));
            }
        }

        public int user_id
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(user_id));
            }
        }

        public int event_id
        {
            get { return _eventId; }
            set
            {
                _eventId = value;
                OnPropertyChanged(nameof(event_id));
            }
        }
    }
}
