using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services
{
    public interface IVectorBuilder<T> where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, ISpanFormattable
    {
        IVector<T> Build();
    }
}
