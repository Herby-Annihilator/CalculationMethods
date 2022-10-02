using CalculationMethods.Core.Services.Factories;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Infrastructure.Services.Factories.String;
using CalculationMethods.Infrastructure.Services.Repositories.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Factories.Base
{
    public class StringFactory : IFactory<string>
    {
        private IConfiguration _configuration;
        public StringFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IRepositoryFactory<string> FileRepositoryFactory(string path)
        {
            return new StringFileRepositoryFactory(path, _configuration
                .GetSection(nameof(VectorFileOptions))
                .Get<VectorFileOptions>());
        }

        public IRepositoryFactory<string> MockRepositoryFactory()
        {
            throw new NotImplementedException();
        }
    }
}
