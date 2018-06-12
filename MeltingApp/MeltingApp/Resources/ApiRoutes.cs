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
            //public const string ShowEvent = "ShowEvent";
            public const string ConfirmAssistance = "ConfirmAssitance";
            public const string UnconfirmAssistance = "UnconfirmAssistance";
            public const string GetUsersAssistance = "GetUserAssistance";
            public const string ShowFacultyInfo = "ShowFacultyInfo";
            public const string ShowUniversityInfo = "ShowUniversityInfo";
            public const string GetAllEvents = "GetAllEvents";
            public const string CreateComment = "CreateComment";
            public const string GetEventComments = "GetEventComments";
            public const string CreateProfileUser = "CreateProfile";
            public const string GetUniversities = "GetUniversities";
            public const string GetFaculties = "GetFaculties";
            public const string SearchUniversities = "SearchUniversities";
            public const string SearchFaculties = "SearchFaculties";
            public const string SearchUsers = "SearchUsers";
            public const string SearchEvents= "SearchEvents";
            public const string DeleteComment = "DeleteComment";
            public const string DeleteAccount = "DeleteAccount";
            public const string ModifyEvent = "ModifyEvent";
            public const string ResetPass = "ResetPass";
        }

        public struct Prefix
        {
            public static string Users = $"/users/{UriParameters.UserId}";
            public static string Universities = $"/universities/{UriParameters.UniversityId}";
            public static string University_id = $"/{UriParameters.OnlyUniversityId}";
            public static string Event_id = $"/events/{UriParameters.EventId}";
            public static string Comment_id = $"/comments/{UriParameters.CommentId}";
        }

        public struct UriParameters
        {
            public const string UserId = "[User_id]";
            public const string UniversityId = "[University_id]";
            public const string OnlyUniversityId = "[OnlyUniversity_id]";
            public const string EventId = "[Event_id]";
            public const string CommentId = "[Comment_id]";
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
            public const string CreateEvent = "/events";     
            public const string GetProfileUser = "/profile";
            public const string EditProfileUser = "/profile";
            public const string AvatarProfileUser = "/profile/avatar";
            public const string GetAllEvents = "/events";
            //public const string ShowEvent = "/events/5";
            public const string GetUserEvents = "/users/5/events";
            public const string ConfirmAssitance = "/votes";
            public const string UnconfirmAssistance = "/votes/self";
            public const string GetUsersAssistance = "/attendees";
            public const string ShowFacultyInfo = "/profile/faculty";
            public const string ShowUniversityInfo = "/locations";
            public const string CreateComment = "/comments";
            public const string GetEventComments = "/comments";
            public const string CreateProfileUser = "/profile";
            public const string GetUniversities = "/locations/universities";
            public const string GetFacultiesfirstpath = "/locations";
            public const string GetFacultiessecondpath = "/faculties";
            public const string SearchUniversities = "/search/universities";
            public const string SearchFaculties = "/search/faculties";
            public const string SearchUsers = "/search/profiles";
            public const string SearchEvents = "/search/events";
            public const string ResetPass = "/auth/reset";
        }

    }
}