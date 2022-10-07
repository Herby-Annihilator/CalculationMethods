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
        protected string FileName { get; set; }
        protected string ApiPath { get; }
        private WebRepository<IMatrix<TElement>> repository;
        public WebMatrixRepository(HttpClient client, string fileName)
        {
            FileName = fileName;
            ApiPath = $"matrix/{FileName}";
            repository = new WebRepository<IMatrix<TElement>>(ApiPath, client);
        }

        public void Save(IMatrix<TElement> matrix) => repository.Save(matrix);

        public IMatrix<TElement> Get() => repository.Get();

        public bool Update(IMatrix<TElement> matrix) => repository.Update(matrix);

        public bool Delete(IMatrix<TElement> matrix) => repository.Delete(matrix);
    }
}
