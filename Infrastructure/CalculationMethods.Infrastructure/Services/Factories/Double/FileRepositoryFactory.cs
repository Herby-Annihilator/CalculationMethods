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
    public class FileRepositoryFactory : IRepositoryFactory<double>
    {
        private string _path;
        private MatrixFromFileOptions _options;
        private VectorFileOptions _vectorFileOptions;
        public FileRepositoryFactory(string path, MatrixFromFileOptions options, VectorFileOptions vectorFileOptions)
        {
            _path = path;
            _options = options;
            _vectorFileOptions = vectorFileOptions;
        }

        public IMatrixRepository<IMatrix<double>, double> CreateMatrixRepository()
        {
            return new DoubleMatrixRepository(_path, _options);
        }

        public IMatrixRepository<ISquareMatrix<double>, double> CreateSquareMatrixRepository()
        {
            return new DoubleSquareMatrixRepository(_path, _options);
        }

        public IVectorRepository<double> CreateVectorRepository()
        {
            return new DoubleVectorRepository(_path, _vectorFileOptions);
        }
    }
}
