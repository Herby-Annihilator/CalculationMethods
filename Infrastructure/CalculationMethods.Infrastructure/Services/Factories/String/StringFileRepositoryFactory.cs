using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Services.Repositories.Options;
using CalculationMethods.Infrastructure.Services.Repositories.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Factories.String
{
    public class StringFileRepositoryFactory : IRepositoryFactory<string>
    {
        private string _path;
        private VectorFileOptions _options;
        public StringFileRepositoryFactory(string path, VectorFileOptions options)
        {
            _path = path;
            _options = options;
        }
        public IMatrixRepository<IMatrix<string>, string> CreateMatrixRepository()
        {
            throw new NotImplementedException();
        }

        public IMatrixRepository<ISquareMatrix<string>, string> CreateSquareMatrixRepository()
        {
            throw new NotImplementedException();
        }

        public IVectorRepository<string> CreateVectorRepository()
        {
            return new StringVectorRepository(_path, _options);
        }
    }
}
