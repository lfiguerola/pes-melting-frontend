using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class Event : EntityBase
    {
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
        //private ?attendee[]  _attendees;
        //private ?comment[] _comments;


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
        /// the event karma
        /// </summary>
        /*public int karma
        {
            get { return _karma; }
            set
            {
                _karma = value;
                OnPropertyChanged(nameof(karma));
            }
        }*/
        
        
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

        /// <summary>
        /// the event location
        /// </summary>
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

        /// <summary>
        /// the user name
        /// </summary>
        /*public DateTime dateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(dateTime));
            }
        }*/


    }
}
