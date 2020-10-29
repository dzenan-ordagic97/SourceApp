using Flurl.Http;
using Newtonsoft.Json;
using SourceApp.Mobile.Helpers;
using SourceApp.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace SourceApp.Mobile.Services
{
    public class RestService
    {
        public static string Email;
        public static class Session
        {
            public static int id { get; set; }
            public static string username { get; set; }
            public static string email { get; set; }
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
        public async Task<T> GetQuery<T>(object search)
        {
            var url = $"{_apiURL}/{_route}";
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
        public async Task<T> GetQuery2<T>(object search, object search2)
        {
            var url = $"{_apiURL}/{_route}";
            if (search != null && search2 != null)
            {
                url += "?";
                url += await search.ToQueryString();
                url += "&";
                url += await search2.ToQueryString();
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
        public async Task<ResponseWithPaging<T>> GetWithHeader<T>(object search, int next)
        {
            var url = $"{_apiURL}/{_route}";
            if (search != null)
            {
                url += "?";
                url += await search.ToQueryString();
            }
            //var result = await url.GetJsonAsync<T>();
            //return result;
            try
            {
                var responseData = new ResponseWithPaging<T>() {
                    IsFirst = false,
                    IsLast = false
                };
                var response = await url.GetAsync().ContinueWith(async res =>
                {
                    var header = res.Result.Headers.GetValues("Link");
                    var data = await res.ReceiveJson<List<T>>();
                    return new { data = data, header = header };
                });
                var linkHeader = response.Result.header.FirstOrDefault().Split(';');
                Uri _previous = null;
                Uri _next = null;
                Uri _last = null;
                string paramPrevious = "1" ;
                string paramNext = "1";
                string paramLast = "1";

                if (next > 1 && response.Result.header.FirstOrDefault().Contains("next"))
                {
                    _next = new Uri(linkHeader[2].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                    _previous = new Uri(linkHeader[1].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                    _last = new Uri(linkHeader[3].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                }
                if(next == 1)
                {
                    _next = new Uri(linkHeader[1].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                    _last = new Uri(linkHeader[2].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                    
                }

                if(!response.Result.header.FirstOrDefault().Contains("next"))
                {
                    _previous = new Uri(linkHeader[1].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                    _last = new Uri(linkHeader[2].Split(',')[1].Replace('>', ' ').Replace('<', ' '));
                }
                if(next > 1 && response.Result.header.FirstOrDefault().Contains("next"))
                {
                    paramPrevious = HttpUtility.ParseQueryString(_previous.Query).Get("_page");
                    paramNext = HttpUtility.ParseQueryString(_next.Query).Get("_page");
                    paramLast = HttpUtility.ParseQueryString(_last.Query).Get("_page");
                }
                if (next == 1)
                {
                    paramPrevious = "1";
                    paramNext = HttpUtility.ParseQueryString(_next.Query).Get("_page");
                    paramLast = HttpUtility.ParseQueryString(_last.Query).Get("_page");
                    responseData.IsFirst = true;
                }
                if (!response.Result.header.FirstOrDefault().Contains("next"))
                {
                    paramLast = HttpUtility.ParseQueryString(_last.Query).Get("_page");
                    paramPrevious = HttpUtility.ParseQueryString(_previous.Query).Get("_page");
                    paramNext = paramLast;
                    responseData.IsLast = true;
                }


                responseData.Data = response.Result.data;
                responseData.Next = int.Parse(paramNext);
                responseData.Last = int.Parse(paramLast);
                responseData.Previous = int.Parse(paramPrevious);
                return responseData;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized!", "OK");
                }
                return null;
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
