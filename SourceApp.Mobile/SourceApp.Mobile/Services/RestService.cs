using Flurl.Http;
using Newtonsoft.Json;
using SourceApp.Mobile.Helpers;
using SourceApp.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SourceApp.Mobile.Services
{
    public class RestService
    {
        public static string Email;
        public static class Session
        {
            public static string ImePrezime { get; set; }
            public static string JWT { get; set; }
            public static List<string> Role { get; set; }
        }
        private string _route;
        private string _apiURL = Constant.RestUrl;

        public User User { get; set; }
        public RestService(string route)
        {
            _route = route;
        }
        public async Task<T> Login<T>(object search)
        {
            var url = $"{_apiURL}/{_route}";
            if (search == null)
                return default(T);

            if (search != null)
            {
                url += "?";
                url += await search.ToQueryString();
            }
            //var result = await url.GetJsonAsync<T>();
            //return result;
            try
            {
                return await url.GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized!", "OK");
                }
                return default(T);
            }
        }
        public async Task<T> Get<T>(object search)
        {
            var url = $"{_apiURL}/{_route}";
            if (search != null)
            {
                url += "?";
                url += await search.ToQueryString();
            }
            var result = await url.GetJsonAsync<T>();
            return result;
           
        }
        public async Task<T> GetById<T>(object id)
        {
            var url = $"{_apiURL}/{_route}/{id}";
            try
            {
                return await url.GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized!", "OK");
                }
                return default(T);
            }
        }
        public async Task<T> Insert<T>(object request)
        {
            var url = $"{_apiURL}/{_route}";

            try
            {
                return await url.PostJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }
                await Application.Current.MainPage.DisplayAlert("Error", stringBuilder.ToString(), "OK");
                return default(T);
            }
        }
        public async Task<T> Update<T>(int id, object request)
        {
            var url = $"{_apiURL}/{_route}/{id}";
            try
            {
                return await url.PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();
                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }
                await Application.Current.MainPage.DisplayAlert("Error", stringBuilder.ToString(), "OK");
                return default(T);
            }
        }
        public async Task<T> Delete<T>(int id)
        {
            var url = $"{_apiURL}/{_route}/{id}";
            try
            {
                return await url.DeleteAsync().ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }
                await Application.Current.MainPage.DisplayAlert("Error", stringBuilder.ToString(), "OK");
                return default(T);
            }
        }
    }
}
