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
            public const string GetAllEvents = "GetAllEvents";
            public const string ShowEvent = "ShowEvent";
            public const string ConfirmAssistance = "ConfirmAssitance";
            public const string GetUserAssistance = "GetUserAssistance";
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
            public const string GetAllEvents = "/users/1/events";
            public const string ShowEvent = "/events/2/event";
            public const string ConfirmAssitance = "/users/1/events/2/votes";
            public const string GetUserAssistance = "/users/1/events/2";
        }
    }
}