using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services
{
    public interface IMatrixBuilder<T> where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, ISpanFormattable
    {
        IMatrix<T> Build();
    }
}
