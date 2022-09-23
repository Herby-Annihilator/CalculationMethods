using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services.Factories
{
    public interface IRepositoryFactory<TElement>
    {
        IMatrixRepository<IMatrix<TElement>, TElement> CreateMatrixRepository();
        IMatrixRepository<ISquareMatrix<TElement>, TElement> CreateSquareMatrixRepository();

        IVectorRepository<TElement> CreateVectorRepository();
    }
}
