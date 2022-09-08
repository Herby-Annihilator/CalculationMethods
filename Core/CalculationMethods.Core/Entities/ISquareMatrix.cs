using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Entities
{
    public interface ISquareMatrix : IMatrix
    {
        int Size { get; }
    }

    public interface ISquareMatrix<T> : ISquareMatrix, IMatrix<T>
        where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, ISpanFormattable
    {
        ISquareMatrix<T> GetLMatrix();
        ISquareMatrix<T> GetUMatrix();
    }
}
