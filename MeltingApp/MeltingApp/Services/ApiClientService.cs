using MeltingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.Exceptions;
using MeltingApp.Models;
using MeltingApp.Resources;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MeltingApp.Services
{
    public class ApiClientService : IApiClientService 
    {
        public HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = new Uri("https://melting-app.herokuapp.com")
        };

        public Dictionary<Tuple<Type, string>, string> UrlPostDictionary { get; set; } = new Dictionary<Tuple<Type, string>, string>
        {
            {new Tuple<Type, string>(typeof(User), ApiRoutes.ActivateUserMethodName), ApiRoutes.ActivateUserEndpoint },
            {new Tuple<Type, string>(typeof(User), ApiRoutes.RegisterUserMethodName), ApiRoutes.RegisterUserEndpoint },
        };

        public Dictionary<Type, string> UrlPutDictionary { get; set; } = new Dictionary<Type, string>()
        {
           
        };

        public Dictionary<Type, string> UrlGetDictionary { get; set; } = new Dictionary<Type, string>()
        {
            
        };

        public Dictionary<Type, string> UrlDeleteDictionary { get; set; } = new Dictionary<Type, string>()
        {
            
        };

        public async Task<T> PostAsync<T>(T entity, string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = null;
            string postResult = null; 
            try
            {
                result = await HttpClient.PostAsync(new Uri(GetPostUri<T>(methodName)), content);
                postResult = await result.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<T>(postResult);
                if (result.IsSuccessStatusCode)
                {
                    string responseMessage = null;
                    if (deserializedObject == null)
                    {
                        responseMessage = postResult;
                    }
                    successResultCallback?.Invoke(true, responseMessage);
                }
                else successResultCallback?.Invoke(false, postResult);
                return deserializedObject;
            }
            catch (Exception)
            {
                throw new ApiClientException(postResult);
            }
        }

        //public async Task<bool?> DeleteAsync<T>(T entity) where T : EntityBase
        //{
        //    HttpResponseMessage result = null;
        //    try
        //    {
        //        result = await HttpClient.DeleteAsync(new Uri($"{GetDeleteUri<T>()}/{entity.Key}"));
        //    }
        //    catch (Exception)
        //    {
        //        DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred deleting {typeof(T)}. Check internet connection.");
        //    }
        //    return result?.IsSuccessStatusCode;
        //}



        //public async Task<bool?> PutAsync<T>(T entity) where T : EntityBase
        //{
        //    var json = JsonConvert.SerializeObject(entity);
        //    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage result = null;

        //    try
        //    {
        //        result = await HttpClient.PutAsync(new Uri($"{GetPutUri<T>()}/{entity.Key}"), content);
        //    }
        //    catch (Exception)
        //    {
        //        DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred updating {typeof(T)}. Check internet connection.");
        //    }
        //    return result?.IsSuccessStatusCode;
        //}

        public async Task<List<T>> GetAsync<T>() where T : EntityBase
        {
            HttpResponseMessage result = null;
            try
            {
                result = await HttpClient.GetAsync(new Uri(GetGetUri<T>()));
            }
            catch (Exception)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred getting {typeof(T)} from server. Check internet connection.");
            }
            return result?.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<List<T>>(await result.Content.ReadAsStringAsync()) : null;
        }

        private string GetDeleteUri<T>()
        {
            return UrlDeleteDictionary[typeof(T)];
        }
        private string GetPutUri<T>()
        {
            return UrlPutDictionary[typeof(T)];
        }

        private string GetPostUri<T>(string methodName)
        {
            foreach (var key in UrlPostDictionary.Keys)
            {
                if (key.Item1 == typeof(T) && key.Item2.Equals(methodName))
                {
                    return UrlPostDictionary[key];
                }
            }
            return null;
        }

        private string GetGetUri<T>()
        {
            return UrlGetDictionary[typeof(T)];
        }

    }
}
