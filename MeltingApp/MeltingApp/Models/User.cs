using System;
using System.Collections.Generic;
using System.Text;

namespace MeltingApp.Models
{
    public class User : EntityBase
    {
        /// <summary>
        /// the user name
        /// </summary>
        public string username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(username));
            }
        }

        /// <summary>
        /// the user email in format user@example.com
        /// </summary>
        public string email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(email));
            }
        }

        /// <summary>
        /// the user password
        /// </summary>
        public string password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
            }
        }

        /// <summary>
        /// indicates if this user is activated or not
        /// </summary>
        public bool activated
        {
            get { return activated; }
            set
            {
                activated = value;
                OnPropertyChanged(nameof(activated));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged(nameof(code));
            }
        }

    }
}
