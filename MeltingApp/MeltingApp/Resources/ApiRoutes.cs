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
            public const string GetProfileUser = "GetProfile";
            public const string EditProfileUser = "EditProfile";
            public const string AvatarProfileUser = "AvatarProfile";
            public const string GetUniversities = "GetUniversities";
            public const string GetFaculties = "GetFaculties";
        }

        /// <summary>
        /// Endpoints
        /// </summary>
        public struct Endpoints
        {
            public const string ActivateUser = "/auth/activate";
            public const string RegisterUser = "/auth/register";
            public const string LoginUser = "/auth/login";
            public const string GetProfileUser = "/profile";
            public const string EditProfileUser = "/profile";
            public const string AvatarProfileUser = "/profile/avatar";
            public const string GetUniversities = "/locations/universities";
            public const string GetFaculties = "locations/universities";
        }

    }
}