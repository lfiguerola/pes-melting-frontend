using System;

namespace MeltingApp.Exceptions
{
    public class ApiClientException : Exception
    {
        public ApiClientException(string message) : base(message)
        {
        }

        
    }
}
