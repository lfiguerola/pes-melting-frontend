using System;
using System.Threading.Tasks;
using MeltingApp.Resources;

namespace MeltingApp.Interfaces
{
    public interface IApiClientService
    {
        Task<TResult> PostAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null, MeltingUriParser meltingUriParser = null);
        Task<TResult> GetAsync<TRequest, TResult>(string methodName, Action<bool, string> successResultCallback = null, MeltingUriParser meltingUriParser = null);
        Task<TResult> PutAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null, MeltingUriParser meltingUriParser = null);
        Task<TResult> DeleteAsync<TRequest, TResult>(string methodName, Action<bool, string> successResultCallback = null, MeltingUriParser meltingUriParser = null);
    }
}
