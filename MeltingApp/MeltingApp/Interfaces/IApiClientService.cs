using System;
using System.Threading.Tasks;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IApiClientService
    {
        Task<T> PostAsync<T>(T entity, string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase;
        Task<T> GetAsync<T>(string methodName, Action<bool, string> successResultCallback = null) ;
        Task<T> PutAsync<T>(T entity, string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase;
    }
}
