using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Web
{
    public class WebMatrixRepository<TElement> : IMatrixRepository<IMatrix<TElement>, TElement>
    {
        protected HttpClient Client { get; set; }
        protected string FileName { get; set; }
        protected string ApiPath { get; }
        public WebMatrixRepository(HttpClient client, string fileName)
        {
            Client = client;
            FileName = fileName;
            ApiPath = $"matrix/{FileName}";
        }

        public void Save(IMatrix<TElement> matrix)
        {
            Client.PostAsJsonAsync(ApiPath, matrix).Result.EnsureSuccessStatusCode();
        }

        public IMatrix<TElement> Get()
        {
            return Client.GetFromJsonAsync<IMatrix<TElement>>(ApiPath).Result;
        }

        public bool Update(IMatrix<TElement> matrix)
        {
            return Client.PutAsJsonAsync(ApiPath, matrix)
                .Result
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;
        }

        public bool Delete(IMatrix<TElement> matrix)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, ApiPath)
            {
                Content = JsonContent.Create(matrix),
            };
            return Client.SendAsync(request)
                .Result
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;
        }
    }
}
