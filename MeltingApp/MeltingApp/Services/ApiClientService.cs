using MeltingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.Exceptions;
using MeltingApp.Models;
using MeltingApp.Resources;
using Newtonsoft.Json;

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
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.ActivateUser), ApiRoutes.Endpoints.ActivateUser },
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.RegisterUser), ApiRoutes.Endpoints.RegisterUser },
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.LoginUser), ApiRoutes.Endpoints.LoginUser },
            //TODO: Remove this fake url
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.AvatarProfileUser), "users/1" + ApiRoutes.Endpoints.AvatarProfileUser }
        };

        public Dictionary<Tuple<Type, string>, string> UrlPutDictionary { get; set; } = new Dictionary<Tuple<Type, string>, string>()
        {
            //TODO: Remove this fake url
            { new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.EditProfileUser), "/users/1" + ApiRoutes.Endpoints.EditProfileUser }
        };

        public Dictionary<Tuple<Type, string>, string> UrlGetDictionary { get; set; } = new Dictionary<Tuple<Type, string>,string>
        {
            //TODO: Remove this fake url
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.GetProfileUser), "/users/1" + ApiRoutes.Endpoints.GetProfileUser }
        };

        public Dictionary<Type, string> UrlDeleteDictionary { get; set; } = new Dictionary<Type, string>()
        {

        };

        public async Task<TResult> PostAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            ApiResponseMessage responseMessage = null;
            string postResult = null;
            try
            {
                var result = await HttpClient.PostAsync(new Uri(GetPostUri<TRequest>(methodName)), content);
                postResult = await result.Content.ReadAsStringAsync();
                TResult deserializedObject = null;
                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<TResult>(postResult, jsonSerializerSettings);
                }
                catch (JsonSerializationException)
                {
                    responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(postResult);
                }

                if (result.IsSuccessStatusCode)
                {
                    successResultCallback?.Invoke(true, responseMessage?.message);
                }
                else successResultCallback?.Invoke(false, responseMessage?.message.Equals(string.Empty) ?? true ? result.ReasonPhrase : responseMessage?.message);

                return deserializedObject;
            }
            catch (Exception)
            {
                throw new ApiClientException(postResult);
            }
        }

        public async Task<TResult> GetAsync<TRequest,TResult>(string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase
        {
            ApiResponseMessage responseMessage = null;
            string getResult = null;
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };

            try
            {
                var result = await HttpClient.GetAsync(new Uri(GetGetUri<TRequest>(methodName)));
                getResult = await result.Content.ReadAsStringAsync();
                TResult deserializedObject = null;

                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<TResult>(getResult, jsonSerializerSettings);
                }
                catch (JsonSerializationException)
                {
                    responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(getResult);
                }

                if (result.IsSuccessStatusCode)
                {
                    successResultCallback?.Invoke(true, responseMessage?.message);
                }
                else successResultCallback?.Invoke(false, responseMessage?.message.Equals(string.Empty) ?? true ? result.ReasonPhrase : responseMessage?.message);

                return deserializedObject;
            }
            catch (Exception)
            {
                throw new ApiClientException(getResult);
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



        public async Task<TResult> PutAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            ApiResponseMessage responseMessage = null;
            string putResult = null;
            try
            {
                var result = await HttpClient.PutAsync(new Uri(GetPutUri<TRequest>(methodName)), content);
                putResult = await result.Content.ReadAsStringAsync();
                TResult deserializedObject = null;
                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<TResult>(putResult, jsonSerializerSettings);
                }
                catch (JsonSerializationException)
                {
                    responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(putResult);
                }

                if (result.IsSuccessStatusCode)
                {
                    successResultCallback?.Invoke(true, responseMessage?.message);
                }
                else successResultCallback?.Invoke(false, responseMessage?.message.Equals(string.Empty) ?? true ? result.ReasonPhrase : responseMessage?.message);

                return deserializedObject;
            }
            catch (Exception)
            {
                throw new ApiClientException(putResult);
            }
        }


        private string GetDeleteUri<T>()
        {
            return UrlDeleteDictionary[typeof(T)];
        }
        private string GetPutUri<T>(string methodName)
        {
            foreach (var key in UrlPutDictionary.Keys)
            {
                if (key.Item1 == typeof(T) && key.Item2.Equals(methodName))
                {
                    return UrlPutDictionary[key];
                }
            }
            return null;
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

        private string GetGetUri<T>(string methodName)
        {
            foreach (var key in UrlGetDictionary.Keys)
            {
                if (key.Item1 == typeof(T) && key.Item2.Equals(methodName))
                {
                    return UrlGetDictionary[key];
                }
            }
            return null;
        }

    }
}