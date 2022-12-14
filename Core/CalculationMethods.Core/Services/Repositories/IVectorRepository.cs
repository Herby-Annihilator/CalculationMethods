using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services.Repositories
{
    public interface IVectorRepository<TElement>
    {
        void Save(IVector<TElement> vector);
        bool Update(IVector<TElement> vector);
        bool Delete(IVector<TElement> vector);
        IVector<TElement> Get();
    }
}
