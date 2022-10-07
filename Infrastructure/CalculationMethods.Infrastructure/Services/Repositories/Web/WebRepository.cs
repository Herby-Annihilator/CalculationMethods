using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Web
{
    internal class WebRepository<T>
    {
        protected string ApiPath { get; set; }
        protected HttpClient HttpClient { get; set; }
        internal WebRepository(string apiPath, HttpClient client)
        {
            ApiPath = apiPath;
            HttpClient = client;
        }
        internal T Get()
        {
            return HttpClient.GetFromJsonAsync<T>(ApiPath).Result;
        }

        internal bool Delete(T item)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, ApiPath)
            {
                Content = JsonContent.Create(item)
            };
            HttpResponseMessage response = HttpClient.SendAsync(request).Result.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<bool>().Result;
        }

        internal bool Update(T item)
        {
            return HttpClient.PutAsJsonAsync(ApiPath, item)
                .Result
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;
        }

        internal void Save(T item)
        {
            HttpClient.PostAsJsonAsync(ApiPath, item).Result.EnsureSuccessStatusCode();
        }
    }
}
