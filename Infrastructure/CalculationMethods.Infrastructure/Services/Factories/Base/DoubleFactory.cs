using CalculationMethods.Core.Services.Factories;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Infrastructure.Services.Factories.Double;
using CalculationMethods.Infrastructure.Services.Repositories.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Factories.Base
{
    public class DoubleFactory : IFactory<double>
    {
        private IConfiguration _configuration;
        public DoubleFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IRepositoryFactory<double> FileRepositoryFactory(string path)
        {
            MatrixFromFileOptions matrixOptions = _configuration
                .GetSection(nameof(MatrixFromFileOptions))
                .Get<MatrixFromFileOptions>();
            VectorFileOptions vectorOptions = _configuration
                .GetSection(nameof(VectorFileOptions))
                .Get<VectorFileOptions>();
            return new FileRepositoryFactory(path, matrixOptions, vectorOptions);
        }

        public IRepositoryFactory<double> MockRepositoryFactory()
        {
            return new MockRepositoryFactory();
        }
    }
}
