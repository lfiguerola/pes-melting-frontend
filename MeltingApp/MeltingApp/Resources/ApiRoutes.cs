using System;

namespace MeltingApp.Resources
{
    public static class ApiRoutes
    {
        /// <summary>
        /// Methods names for the endpoints
        /// </summary>
        public const string ActivateUserMethodName = "Activate";

        public const string RegisterUserMethodName = "Register";


        /// <summary>
        /// Endpoints
        /// </summary>
        public const string ActivateUserEndpoint = "/auth/activate";

        public const string RegisterUserEndpoint = "/auth/register"; 

    }
}
