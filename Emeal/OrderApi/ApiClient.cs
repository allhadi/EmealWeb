using Emeal.Security;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Emeal.OrderApi
{
    public class ApiClient
    {
        string _apiUrl = "https://localhost:44324";

        public ApiClient(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public T Post<T, T1>(string method, T1 obj) where T : new()
        {
            var response = ApiPost(method, obj);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = new JsonDeserializer().Deserialize<T>(response);
                return result;
            }
            else
            {
                return new T();
            }
        }

        public List<T> PostList<T, T1>(string method, T1 obj)
        {
            var response = ApiPost(method, obj);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = new JsonDeserializer().Deserialize<List<T>>(response);
                return result;
            }
            else
            {
                return new List<T>();
            }
        }

        private IRestResponse ApiPost<T>(string method, T obj)
        {
            var user = AuthenticateTicket.GetCurrentUser();
            var client = new RestClient(_apiUrl);
            var request = new RestRequest(method, Method.POST);
            var data = JsonConvert.SerializeObject(obj);
            if (user != null)
            {
                request.AddHeader("Authorization", user.AccessCode);
            }
            request.AddParameter("application/json; charset=utf-8", data, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.ExecuteAsync(request).Result;
            return response;
        }

        internal List<T> GetList<T>(string method)
        {
            var response = ApiGet(method);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = new JsonDeserializer().Deserialize<List<T>>(response);
                return result;
            }
            else
            {
                return new List<T>();
            }
        }

        private IRestResponse ApiGet(string method)
        {
            var user = AuthenticateTicket.GetCurrentUser();
            var client = new RestClient(_apiUrl);
            var request = new RestRequest(method, Method.GET);
            if (user != null)
            {
                request.AddHeader("Authorization", user.AccessCode);
            }
            var response = client.ExecuteAsync(request).Result;
            return response;
        }
    }
}