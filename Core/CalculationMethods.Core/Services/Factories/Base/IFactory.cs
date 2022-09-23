using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services.Factories.Base
{
    public interface IFactory<TElement>
    {
        IRepositoryFactory<TElement> FileRepositoryFactory(string path);

        IRepositoryFactory<TElement> MockRepositoryFactory();
    }
}
