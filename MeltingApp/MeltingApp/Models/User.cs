using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MeltingApp.Models
{
    public class User : EntityBase
    {
        private string _username;
        private string _email;
        private string _password;
        private bool _activated;
        private string _code;
        private string _full_name;
        private string _biography;
        private int _karma;
        private string _country_code;
        private string _university;
        private string _faculty;
        private string _avatarURL;
        private string _name;
        private string _address;
        //TODO:long? double?
        private int _latitude;
        private int _longitude;
 
        /// <summary>
        /// the user name
        /// </summary>
        public string username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(username));
            }
        }

        /// <summary>
        /// the user email in format user@example.com
        /// </summary>
        public string email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(email));
            }
        }

        /// <summary>
        /// the user password
        /// </summary>
        public string password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(password));
            }
        }

        /// <summary>
        /// indicates if this user is activated or not
        /// </summary>
        public bool activated
        {
            get => _activated;
            set
            {
                _activated = value;
                OnPropertyChanged(nameof(activated));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(code));
            }
        }

        public string fullname
        {
            get => _full_name;
            set
            {
                _full_name = value;
                OnPropertyChanged(nameof(fullname));
            }
        }
        public string biography
        {
            get => _biography;
            set
            {
                _biography = value;
                OnPropertyChanged(nameof(biography));
            }
        }
        public int karma
        {
            get => _karma;
            set
            {
                _karma = value;
                OnPropertyChanged(nameof(karma));
            }
        }
        public string countrycode
        {
            get => _country_code;
            set
            {
                _country_code = value;
                OnPropertyChanged(nameof(countrycode));
            }
        }
        public string university
        {
            get => _university;
            set
            {
                _university = value;
                OnPropertyChanged(nameof(university));
            }
        }
        public string faculty
        {
            get => _faculty;
            set
            {
                _faculty = value;
                OnPropertyChanged(nameof(faculty));
            }
        }
        public string avatarURL
        {
            get => _avatarURL;
            set
            {
                _avatarURL = value;
                OnPropertyChanged(nameof(avatarURL));
            }
        }
        public string name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }
        public string address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(address));
            }
        }
        public int latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(latitude));
            }
        }
        public int longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(longitude));
            }
        }
    }
}
