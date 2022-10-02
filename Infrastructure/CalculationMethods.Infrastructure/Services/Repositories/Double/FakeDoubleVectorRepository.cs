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
    public class FakeDoubleVectorRepository : IVectorRepository<double>
    {
        public bool Delete(IVector<double> vector)
        {
            return true;
        }

        public IVector<double> Get()
        {
            int size = 5;
            IVector<double> vector = new DoubleVector(size);
            for (int i = 0; i < size; i++)
            {
                vector[i] = i;
            }
            return vector;
        }

        public void Save(IVector<double> vector)
        {
            
        }

        public bool Update(IVector<double> vector)
        {
            return true;
        }
    }
}
