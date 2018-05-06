using System;
using System.Threading.Tasks;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IApiClientService
    {
        Task<TResult> PostAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase;
        Task<TResult> GetAsync<TRequest, TResult>(string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase;
        Task<TResult> PutAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase;
    }
}
