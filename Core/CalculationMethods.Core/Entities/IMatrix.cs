using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Entities
{
    public interface IMatrix
    {
    }

    public interface IMatrix<T> : IMatrix
    {
        int RowsCount { get; }
        int ColsCount { get; }
        T this[int row, int column] { get; set; }

        T Determinant();
        T Norm();
        T ConditionNumber();
        IMatrix<T> Inverse();
    }
}
