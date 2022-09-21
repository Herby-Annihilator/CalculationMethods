using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Entities
{
    public interface IVector
    {
        int Size { get; }
    }

    public interface IVector<T> : IVector
    {
        T this[int index] { get; set; }
    }
}
