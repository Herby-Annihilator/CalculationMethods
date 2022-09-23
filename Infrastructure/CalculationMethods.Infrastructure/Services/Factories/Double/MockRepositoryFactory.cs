using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Services.Repositories.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Factories.Double
{
    public class MockRepositoryFactory : IRepositoryFactory<double>
    {
        public IMatrixRepository<IMatrix<double>, double> CreateMatrixRepository()
        {
            return new FakeDoubleMatrixRepository();
        }

        public IMatrixRepository<ISquareMatrix<double>, double> CreateSquareMatrixRepository()
        {
            return new FakeDoubleSquareMatrixRepository();
        }

        public IVectorRepository<double> CreateVectorRepository()
        {
            return new FakeDoubleVectorRepository();
        }
    }
}
