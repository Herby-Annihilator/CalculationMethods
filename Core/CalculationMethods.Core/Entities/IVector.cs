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
        where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, ISpanFormattable
    {
        T this[int index] { get; set; }
    }
}
