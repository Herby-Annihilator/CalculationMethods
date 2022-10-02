using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Double
{
    public class FakeDoubleMatrixRepository : IMatrixRepository<IMatrix<double>, double>
    {
        public bool Delete(IMatrix<double> matrix)
        {
            return true;
        }

        public IMatrix<double> Get() => new FakeDoubleSquareMatrixRepository().Get();

        public void Save(IMatrix<double> matrix)
        {
            
        }

        public bool Update(IMatrix<double> matrix)
        {
            return true;
        }
    }
}
