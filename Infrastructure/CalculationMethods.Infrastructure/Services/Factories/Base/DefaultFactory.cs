using CalculationMethods.Core.Services.Factories;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Infrastructure.Services.Factories.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Factories.Base
{
    public class DefaultFactory : IFactory<double>
    {
        public IRepositoryFactory<double> FileRepositoryFactory(string path)
        {
            return new FileRepositoryFactory(path, null, null);
        }

        public IRepositoryFactory<double> MockRepositoryFactory()
        {
            return new MockRepositoryFactory();
        }
    }
}
