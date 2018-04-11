using System;
using MeltingApp.Interfaces;
using Xamarin.Forms;

namespace MeltingApp.Exceptions
{
    public class ApiClientException : Exception
    {
        public ApiClientException(string message) : base(message)
        {
            DependencyService.Get<IOperatingSystemMethods>().ShowToast(message);
        }

        
    }
}
