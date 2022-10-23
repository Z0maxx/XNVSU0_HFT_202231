using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Client
{
    class RestService
    {
        HttpClient client;

        public RestService(string baseurl, string pingableEndpoint = "swagger")
        {
            bool isOk;
            do
            {
                isOk = Ping(baseurl + pingableEndpoint);
            } while (isOk == false);
            Init(baseurl);
        }

        private static bool Ping(string url)
        {
            try
            {
                WebClient wc = new();
                wc.DownloadData(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Init(string baseurl)
        {
            client = new();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }

        public List<T> Get<T>(string endpoint)
        {
            List<T> items;
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(Errors(response)[0]);
            }
            return items;
        }

        public T GetSingle<T>(string endpoint)
        {
            T item;
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(Errors(response)[0]);
            }
            return item;
        }

        public T Get<T>(int id, string endpoint)
        {
            T item;
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                item = JsonSerializer.Deserialize<T>(json, jsonOptions);
            }
            else
            {
                throw new ArgumentException(Errors(response)[0]);
            }
            return item;
        }

        public string[] Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            return Errors(response);
        }

        public string[] Delete(int id, string endpoint)
        {
            HttpResponseMessage response =
                client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            return Errors(response);
        }

        public string[] Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            return Errors(response);
        }

        public static string[] Errors(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new string[] { "Request completed" };
            }
            else if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                var errors = JsonSerializer.Deserialize<RestExceptionInfo1>(json, jsonOptions).Errors;
                return errors.Select(e => e.Value[0]).ToArray();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo2>().GetAwaiter().GetResult();
                return new string[] { error.Msg };
            }
        }
    }
    public class RestExceptionInfo1
    {
        public RestExceptionInfo1()
        {

        }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

    }
    public class RestExceptionInfo2
    {
        public RestExceptionInfo2()
        {

        }
        public string Msg { get; set; }
    }
}
