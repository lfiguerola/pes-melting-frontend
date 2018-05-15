﻿using MeltingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.Exceptions;
using MeltingApp.Models;
using MeltingApp.Resources;
using MeltingApp.ViewModels;
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
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.ActivateUser), ApiRoutes.Endpoints.ActivateUser },
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.RegisterUser), ApiRoutes.Endpoints.RegisterUser },
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.LoginUser), ApiRoutes.Endpoints.LoginUser },
            {new Tuple<Type, string>(typeof(Event), ApiRoutes.Methods.CreateEvent), ApiRoutes.Endpoints.CreateEvent },
            //TODO: Remove this fake url
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.AvatarProfileUser), "users/11" + ApiRoutes.Endpoints.AvatarProfileUser }
        };

        public Dictionary<Tuple<Type, string>, string> UrlPutDictionary { get; set; } = new Dictionary<Tuple<Type, string>, string>()
        {
            //TODO: Remove this fake url
            { new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.EditProfileUser), "/users/11" + ApiRoutes.Endpoints.EditProfileUser }
        };

        public Dictionary<Tuple<Type, string>, string> UrlGetDictionary { get; set; } = new Dictionary<Tuple<Type, string>,string>
        {
            //TODO: Remove this fake url
            {new Tuple<Type, string>(typeof(Event), ApiRoutes.Methods.ShowEvent), ApiRoutes.Endpoints.ShowEvent}
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.GetProfileUser), "/users/11" + ApiRoutes.Endpoints.GetProfileUser }

        };

        public Dictionary<Type, string> UrlDeleteDictionary { get; set; } = new Dictionary<Type, string>()
        {

        };

        public async Task<T> PostAsync<T>(T entity, string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase
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
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjExLCJyb2xlIjoic3R1ZGVudCJ9.WTHO81A7YfIlwdNzik5-roNNU6jBF7u35YoX0tNflTI");
                var result = await HttpClient.PostAsync(new Uri(GetPostUri<T>(methodName)), content);
                postResult = await result.Content.ReadAsStringAsync();
                T deserializedObject = null;
                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<T>(postResult, jsonSerializerSettings);
                }
                catch (JsonSerializationException)
                {
                    responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(postResult);
                }

                if (result.IsSuccessStatusCode)
                {
                    //si es el login i success, guardem token
                    if (methodName == ApiRoutes.Methods.LoginUser)
                    {
                        //token de l'estil a: {"jwt":"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjExLCJyb2xlIjoic3R1ZGVudCJ9.WTHO81A7YfIlwdNzik5-roNNU6jBF7u35YoX0tNflTI"}
                        var token = postResult.Substring(8, postResult.Length - 8 - 2);
                        entity.token = token;

                    }
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

        public async Task<T> GetAsync<T>(string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjYsInJvbGUiOiJzdHVkZW50IiwibGFzdF9zdGF0dXMiOm51bGx9.eX4UcvZYnM-i6QLOBrAi1Qge5DykE5ofDNxxNOGNcEg");
            ApiResponseMessage responseMessage = null;
            string getResult = null;
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjExLCJyb2xlIjoic3R1ZGVudCJ9.WTHO81A7YfIlwdNzik5-roNNU6jBF7u35YoX0tNflTI");
                var result = await HttpClient.GetAsync(new Uri(GetGetUri<T>(methodName)));
                getResult = await result.Content.ReadAsStringAsync();
                T deserializedObject = null;

                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<T>(getResult, jsonSerializerSettings);
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



        public async Task<T> PutAsync<T>(T entity, string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjExLCJyb2xlIjoic3R1ZGVudCJ9.WTHO81A7YfIlwdNzik5-roNNU6jBF7u35YoX0tNflTI");
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            ApiResponseMessage responseMessage = null;
            string putResult = null;
            try
            {
                var result = await HttpClient.PutAsync(new Uri(GetPutUri<T>(methodName)), content);
                putResult = await result.Content.ReadAsStringAsync();
                T deserializedObject = null;
                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<T>(putResult, jsonSerializerSettings);
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