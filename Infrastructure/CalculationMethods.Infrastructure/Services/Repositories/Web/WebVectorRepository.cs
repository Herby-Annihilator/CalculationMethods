using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Web
{
    public class WebVectorRepository<TElement> : IVectorRepository<TElement>
    {
        private WebRepository<IVector<TElement>> _repository;
        public WebVectorRepository(HttpClient client, string fileName)
        {
            _repository = new WebRepository<IVector<TElement>>($"vector/{fileName}", client);
        }
        public bool Delete(IVector<TElement> vector) => _repository.Delete(vector);

        public IVector<TElement> Get() => _repository.Get();

        public void Save(IVector<TElement> vector) => _repository.Save(vector);

        public bool Update(IVector<TElement> vector) => _repository.Update(vector);
    }
}
