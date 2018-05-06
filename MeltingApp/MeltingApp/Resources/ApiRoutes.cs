using System;
using MeltingApp.Models;

namespace MeltingApp.Resources
{
    public static class ApiRoutes
    {

        
        /// <summary>
        /// Methods names for the endpoints
        /// </summary>
        public struct Methods
        {
            public const string ActivateUser = "Activate";
            public const string RegisterUser = "Register";
            public const string LoginUser = "Login";
<<<<<<< HEAD
            public const string CreateEvent = "CreateEvent";
=======
            public const string GetProfileUser = "GetProfile";
            public const string EditProfileUser = "EditProfile";
            public const string AvatarProfileUser = "AvatarProfile";
>>>>>>> 6fffdbec1343be472abcfec7c058d0bacebab950
        }

        /// <summary>
        /// Endpoints
        /// </summary>
        public struct Endpoints
        {
            public const string ActivateUser = "/auth/activate";
            public const string RegisterUser = "/auth/register";
            public const string LoginUser = "/auth/login";
<<<<<<< HEAD
            public const string CreateEvent = "/users/1/events";
=======
            public const string GetProfileUser = "/profile";
            public const string EditProfileUser = "/profile";
            public const string AvatarProfileUser = "/profile/avatar";
>>>>>>> 6fffdbec1343be472abcfec7c058d0bacebab950
        }

    }
}