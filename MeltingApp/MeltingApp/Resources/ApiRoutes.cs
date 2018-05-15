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
            public const string CreateEvent = "CreateEvent";
            public const string GetProfileUser = "GetProfile";
            public const string EditProfileUser = "EditProfile";
            public const string AvatarProfileUser = "AvatarProfile";
<<<<<<< HEAD
            public const string GetAllEvents = "GetAllEvents";
=======
            public const string ShowEvent = "ShowEvent";

>>>>>>> 8dc79781d9a98326deabb5937ca7f7671447747f
        }

        /// <summary>
        /// Endpoints
        /// </summary>
        public struct Endpoints
        {
            public const string ActivateUser = "/auth/activate";
            public const string RegisterUser = "/auth/register";
            public const string LoginUser = "/auth/login";
            public const string CreateEvent = "/users/1/events";     
            public const string GetProfileUser = "/profile";
            public const string EditProfileUser = "/profile";
            public const string AvatarProfileUser = "/profile/avatar";
<<<<<<< HEAD
            public const string GetAllEvents = "/users/1/events";
=======
            public const string ShowEvent = "/events/2/event";

>>>>>>> 8dc79781d9a98326deabb5937ca7f7671447747f
        }

    }
}