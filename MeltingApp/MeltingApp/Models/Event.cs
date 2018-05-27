using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class Event : EntityBase
    {
        private int _event_id;
        private string _title;
        //private int _karma;
        private string _description;
        //private ?photo _photo;
        //private DateTime _dateTime;
        private string _latitude;
        private string _longitude;
        private string _address;
        private string _name;
        private string _date;
        private int _user_id;
        //private ?attendee[]  _attendees;
        //private ?comment[] _comments;


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

        /// <summary>
        /// the event name
        /// </summary>
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(title));
            }
        }

        /// <summary>
        /// the event description
        /// </summary>
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(description));
            }
        }


        public string latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(latitude));
            }
        }

        public string longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(longitude));
            }
        }

        public string address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(address));
            }
        }

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        public string date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(date));
            }
        }

        //[ManyToOne(CascadeOperations = CascadeOperation.All)]
        //public User Owner { get; set; }

        //[ForeignKey(typeof(User))]
        //public int OwnerDbId { get; set; } 
    }
}
