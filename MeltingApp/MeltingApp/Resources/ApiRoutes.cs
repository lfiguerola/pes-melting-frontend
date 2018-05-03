﻿using System;

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
        }

    }
}