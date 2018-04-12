using System;
using System.Threading.Tasks;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IApiClientService
    {
        Task<T> PostAsync<T>(T entity, string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase;
    }
}
