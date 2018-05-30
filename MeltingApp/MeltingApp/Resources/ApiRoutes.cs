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
            public const string ConfirmAssistance = "ConfirmAssitance";
            public const string UnconfirmAssistance = "UnconfirmAssistance";
            public const string GetUserAssistance = "GetUserAssistance";
            public const string ShowFacultyInfo = "ShowFacultyInfo";
            public const string ShowUniversityInfo = "ShowUniversityInfo";
            public const string GetAllEvents = "GetAllEvents";
            public const string CreateComment = "CreateComment";
            public const string GetAllComments = "GetAllComments";
            public const string CreateProfileUser = "CreateProfile";
            public const string GetUniversities = "GetUniversities";
            public const string GetFaculties = "GetFaculties";
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
            public const string GetAllEvents = "/events";
            public const string ShowEvent = "/events/5";
            public const string ConfirmAssitance = "/users/5/events/5/votes";
            public const string UnconfirmAssistance = "/users/5/events/5/vote";
            public const string GetUserAssistance = "/users/5/events/5";
            public const string ShowFacultyInfo = "/users/3/profile/faculty";
            public const string ShowUniversityInfo = "/locations/2";
            public const string CreateComment = "/users/5/events/5/comments";
            public const string GetAllComments = "/events/5/comments";
            public const string CreateProfileUser = "/profile";
            public const string GetUniversities = "/locations/universities";
            public const string GetFaculties = "/locations/universities/";

        }

    }
}