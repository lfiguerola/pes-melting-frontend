using MeltingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.Exceptions;
using MeltingApp.Models;
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
            {new Tuple<Type, string>(typeof(User), "Activate"), "/activate" },
            {new Tuple<Type, string>(typeof(User), "Register"), "/register" },
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

        public async Task<T> PostAsync<T>(T entity, string methodName) where T : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = null;
            try
            {
                result = await HttpClient.PostAsync(new Uri(GetPostUri<T>(methodName)), content);
                var postResult = await result.Content.ReadAsStringAsync();
                //if para contemplar errores,
                if (result.IsSuccessStatusCode) //si codis 20X ---- OK
                {
                    return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync()); //deserializamos nuestro postResult a <T> con JSONConvert y retornamos el resultado

                }
                DependencyService.Get<IOperatingSystemMethods>().ShowToast(postResult);//Show Toast del postResult en caso de fallo
                throw new ApiClientException(postResult);//throw excepcion de la excepcion q toque con el mensaje q trae el resultado
            }
            catch (Exception)
            {
                DependencyService.Get<IOperatingSystemMethods>().ShowToast($"An error has ocurred creating {typeof(T)}. Check internet connection.");
            }

            if (result == null) return null;

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
            }
            //TODO: null or throw custom exception like PostException
            return null;

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
