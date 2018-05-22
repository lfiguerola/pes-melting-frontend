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
            public const string ShowEvent = "ShowEvent";
            public const string ShowFacultyInfo = "ShowFacultyInfo";
            public const string GetAllEvents = "GetAllEvents";
            public const string CreateComment = "CreateComment";
        }

        /// <summary>
        /// Endpoints
        /// </summary>
        public struct Endpoints
        {
            //TODO: Remove this fake url
            public const string ActivateUser = "/auth/activate";
            public const string RegisterUser = "/auth/register";
            public const string LoginUser = "/auth/login";
            public const string CreateEvent = "/users/1/events";     
            public const string GetProfileUser = "/profile";
            public const string EditProfileUser = "/profile";
            public const string AvatarProfileUser = "/profile/avatar";
            public const string ShowEvent = "/events/2/event";
            public const string ShowFacultyInfo = "/locations/universities/2/faculties";
            public const string GetAllEvents = "/users/1/events";
            public const string CreateComment = "/users/5/events/2/comments";
        }

    }
}