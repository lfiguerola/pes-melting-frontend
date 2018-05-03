using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class Event : EntityBase
    {
        private string _name;
        private int _karma;
        private string _description;
        //private ?photo _photo;
        private DateTime _dateTime;
        //private ?Location _location;
        //private ?attendee[]  _attendees;
        //private ?comment[] _comments;


        /// <summary>
        /// the event name
        /// </summary>
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }
        
        
        /// <summary>
        /// the event karma
        /// </summary>
        public int karma
        {
            get { return _karma; }
            set
            {
                _karma = value;
                OnPropertyChanged(nameof(karma));
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
        }/// <summary>
        /// the user name
        /// </summary>
        public DateTime dateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(dateTime));
            }
        }


    }
}
