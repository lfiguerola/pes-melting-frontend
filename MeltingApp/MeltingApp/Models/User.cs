using System;
using System.Collections.Generic;
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
        private int _userId;
        private string _fullName;
        private string _biography;
        private int _karma;
        private string _countryCode;
        private string _university;
        private int _universityId;
        private string _faculty;
        private int _facultyId;
        private string _avatarURL;
       
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
        /// user activation code
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

        public int user_id
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(user_id));
            }
        }

        public string full_name
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(full_name));
            }
        }

        public string biography
        {
            get { return _biography; }
            set
            {
                _biography = value;
                OnPropertyChanged(nameof(biography));
            }
        }

        public int karma
        {
            get { return _karma; }
            set
            {
                _karma = value;
                OnPropertyChanged(nameof(karma));
            }
        }

        public string country_code
        {
            get { return _countryCode; }
            set
            {
                _countryCode = value;
                OnPropertyChanged(nameof(country_code));
            }
        }

        public string university
        {
            get { return _university; }
            set
            {
                _university = value;
                OnPropertyChanged(nameof(university));
            }
        }

        public int university_id
        {
            get { return _universityId; }
            set
            {
                _universityId = value;
                OnPropertyChanged(nameof(university_id));
            }
        }

        public string faculty
        {
            get { return _faculty; }
            set
            {
                _faculty = value;
                OnPropertyChanged(nameof(faculty));
            }
        }

        public int faculty_id
        {
            get { return _facultyId; }
            set
            {
                _facultyId = value;
                OnPropertyChanged(nameof(faculty_id));
            }
        }

        public string avatarURL
        {
            get { return _avatarURL; }
            set
            {
                _avatarURL = value;
                OnPropertyChanged(nameof(avatarURL));
            }
        }
    }
}
