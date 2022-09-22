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
    {
        ISquareMatrix<T> GetLMatrix();
        ISquareMatrix<T> GetUMatrix();
        IVector<T> Solve(IVector<T> b);
    }
}
