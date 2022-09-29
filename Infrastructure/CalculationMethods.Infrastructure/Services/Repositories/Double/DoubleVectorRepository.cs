using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Services.Repositories.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Double
{
    public class DoubleVectorRepository : IVectorRepository<double>
    {
        private string _path;
        private VectorFileOptions _options;
        public DoubleVectorRepository(string path, VectorFileOptions options)
        {
            _path = path;
            _options = options;
        }
        public bool Delete(IVector<double> vector)
        {
            throw new NotImplementedException();
        }

        public IVector<double> Get()
        {
            throw new NotImplementedException();
        }

        public void Save(IVector<double> vector)
        {
            throw new NotImplementedException();
        }

        public bool Update(IVector<double> vector)
        {
            throw new NotImplementedException();
        }
    }

    
}
