using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Entities.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Double
{
    public class FakeDoubleSquareMatrixRepository : IMatrixRepository<ISquareMatrix<double>, double>
    {
        public bool Delete(ISquareMatrix<double> matrix)
        {
            return true;
        }

        public ISquareMatrix<double> Get()
        {
            int size = 5;
            DoubleSquareMatrix matrix = new DoubleSquareMatrix(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = i;
                }
            }
            return matrix;
        }

        public void Save(ISquareMatrix<double> matrix)
        {
            
        }

        public bool Update(ISquareMatrix<double> matrix)
        {
            return true;
        }
    }
}
