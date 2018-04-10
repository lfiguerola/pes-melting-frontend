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

        /// <summary>
        /// the user name
        /// </summary>
        public string username
        {
            get { return _username; }
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
            get { return _email; }
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
            get { return _password; }
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
            get { return _activated; }
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
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged(nameof(code));
            }
        }

    }
}
