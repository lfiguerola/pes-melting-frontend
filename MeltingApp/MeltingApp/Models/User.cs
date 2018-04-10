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
        public string username { get; set; }

        /// <summary>
        /// the user email in format user@example.com
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// the user password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// indicates if this user is activated or not
        /// </summary>
        public bool activated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }

    }
}
