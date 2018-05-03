using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class StaticInfo : EntityBase
    {
        private string _name;
        private string _latitude;
        private string _longitude;
        private string _adress;
        private string _phone;

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
        /// the longitude of the university
        /// </summary>
        public string longitude
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
        public string latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(latitude));
            }
        }

        /// <summary>
        /// the adress of the university
        /// </summary>
        public string adress
        {
            get { return _adress; }
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(adress));
            }
        }

        /// <summary>
        /// the phone of the university
        /// </summary>
        public string phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(phone));
            }
        }
    }
}
