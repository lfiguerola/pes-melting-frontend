using MeltingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MeltingApp.Exceptions;
using MeltingApp.Models;
using MeltingApp.Resources;
using Newtonsoft.Json;
using Xamarin.Forms;
using Exception = System.Exception;

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
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.AvatarProfileUser), "users/11" + ApiRoutes.Endpoints.AvatarProfileUser },
            {new Tuple<Type, string>(typeof(Event), ApiRoutes.Methods.ConfirmAssistance), ApiRoutes.Endpoints.ConfirmAssitance },
            {new Tuple<Type, string>(typeof(Comment), ApiRoutes.Methods.CreateComment), ApiRoutes.Endpoints.CreateComment},
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.CreateProfileUser), "/users/13" + ApiRoutes.Endpoints.CreateProfileUser}
        };

        public Dictionary<Tuple<Type, string>, string> UrlPutDictionary { get; set; } = new Dictionary<Tuple<Type, string>, string>()
        {
            //TODO: Remove this fake url
            { new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.EditProfileUser), "/users/13" + ApiRoutes.Endpoints.EditProfileUser }
        };

        public Dictionary<Tuple<Type, string>, string> UrlGetDictionary { get; set; } = new Dictionary<Tuple<Type, string>,string>
        {
            //TODO: Remove this fake url
            {new Tuple<Type, string>(typeof(Event), ApiRoutes.Methods.ShowEvent), ApiRoutes.Endpoints.ShowEvent},
            {new Tuple<Type, string>(typeof(int), ApiRoutes.Methods.GetUserAssistance), ApiRoutes.Endpoints.GetUserAssistance},
            {new Tuple<Type, string>(typeof(StaticInfo), ApiRoutes.Methods.ShowFacultyInfo), ApiRoutes.Endpoints.ShowFacultyInfo},
            {new Tuple<Type, string>(typeof(StaticInfo), ApiRoutes.Methods.ShowUniversityInfo), ApiRoutes.Endpoints.ShowUniversityInfo},
            {new Tuple<Type, string>(typeof(User), ApiRoutes.Methods.GetProfileUser), "/users/13" + ApiRoutes.Endpoints.GetProfileUser },
            {new Tuple<Type, string>(typeof(IEnumerable<Event>), ApiRoutes.Methods.GetAllEvents), ApiRoutes.Endpoints.GetAllEvents },
            {new Tuple<Type, string>(typeof(IEnumerable<Comment>), ApiRoutes.Methods.GetAllComments), ApiRoutes.Endpoints.GetAllComments},
            {new Tuple<Type, string>(typeof(IEnumerable<University>), ApiRoutes.Methods.GetUniversities), ApiRoutes.Endpoints.GetUniversities},
            {new Tuple<Type, string>(typeof(IEnumerable<Faculty>), ApiRoutes.Methods.GetFaculties), ApiRoutes.Endpoints.GetFaculties +"1/faculties"}
        };

        public Dictionary<Tuple<Type, string>, string> UrlDeleteDictionary { get; set; } = new Dictionary<Tuple<Type, string>, string>()
        {
            {new Tuple<Type, string>(typeof(Event), ApiRoutes.Methods.UnconfirmAssistance), ApiRoutes.Endpoints.UnconfirmAssistance },
        };

        public async Task<TResult> PostAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase
        {
            var json = JsonConvert.SerializeObject(entity);
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEzLCJyb2xlIjoic3R1ZGVudCIsImxhc3Rfc3RhdHVzIjoxNTI3MzM4NDA5fQ.lbm3GwKE1gb9E8WFIAl12cL3XHLj0E9nfT4lrkV2Fmw");
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
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<TResult> GetAsync<TRequest,TResult>(string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase
        {

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEzLCJyb2xlIjoic3R1ZGVudCIsImxhc3Rfc3RhdHVzIjoxNTI3MzM4NDA5fQ.lbm3GwKE1gb9E8WFIAl12cL3XHLj0E9nfT4lrkV2Fmw");
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

        public async Task<T> DeleteAsync<T>(string methodName, Action<bool, string> successResultCallback = null) where T : EntityBase
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjYsInJvbGUiOiJzdHVkZW50IiwibGFzdF9zdGF0dXMiOm51bGx9.eX4UcvZYnM-i6QLOBrAi1Qge5DykE5ofDNxxNOGNcEg");
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            string deleteResult = null;
            ApiResponseMessage responseMessage = null;
            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjUsInJvbGUiOiJzdHVkZW50IiwibGFzdF9zdGF0dXMiOjB9.yRbv93a6kK4lsBVTrNH-rogHo6_zUxJsTw3vUBKw1Gs");
                var result = await HttpClient.DeleteAsync(new Uri(GetDeleteUri<T>(methodName)));
                deleteResult = await result.Content.ReadAsStringAsync();
                T deserializedObject = null;
                try
                {
                    deserializedObject = JsonConvert.DeserializeObject<T>(deleteResult, jsonSerializerSettings);
                }
                catch (JsonSerializationException)
                {
                    responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(deleteResult);
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
                throw new ApiClientException(deleteResult);
            }
        }


        public async Task<TResult> PutAsync<TRequest, TResult>(TRequest entity, string methodName, Action<bool, string> successResultCallback = null) where TResult : EntityBase where TRequest : EntityBase
        {

            var json = JsonConvert.SerializeObject(entity);
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", @"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEzLCJyb2xlIjoic3R1ZGVudCIsImxhc3Rfc3RhdHVzIjoxNTI3MzM4NDA5fQ.lbm3GwKE1gb9E8WFIAl12cL3XHLj0E9nfT4lrkV2Fmw");
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


       private string GetDeleteUri<T>(string methodName)
        {
            foreach (var key in UrlDeleteDictionary.Keys)
            {
                if (key.Item1 == typeof(T) && key.Item2.Equals(methodName))
                {
                    return UrlDeleteDictionary[key];
                }
            }
            return null;
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