using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class StaticInfo : EntityBase
    {
        private int _locationId;
        private string _alias;
        private string _name;
        private string _adress;
        private string _phone;
        private string _url;
        private float _latitude;
        private float _longitude;

        /// <summary>
        /// the number of the id
        /// </summary>
        public int location_id
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                OnPropertyChanged(nameof(location_id));
            }
        }


        /// <summary>
        /// the name of the alias
        /// </summary>
        public string alias
        {
            get { return _alias; }
            set
            {
                _alias = value;
                OnPropertyChanged(nameof(alias));
            }
        }

        /// <summary>
        /// the url of the faculty
        /// </summary>
        public string url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(url));
            }
        }


        /// <summary>
        /// the name of the university
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
        /// the adress of the university
        /// </summary>
        public string address
        {
            get { return _adress; }
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(address));
            }
        }

        /// <summary>
        /// the phone of the university
        /// </summary>
        public string telephone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(telephone));
            }
        }

        /// <summary>
        /// the longitude of the university
        /// </summary>
        public float longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(longitude));
            }
        }

        /// <summary>
        /// the latitude of the university
        /// </summary>
        public float latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(latitude));
            }
        }
    }
}
