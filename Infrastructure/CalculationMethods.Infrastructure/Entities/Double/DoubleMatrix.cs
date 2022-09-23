using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Entities.Double
{
    public class DoubleMatrix : IMatrix<double>
    {
        public DoubleMatrix(int rowsCount, int colsCount)
        {
            RowsCount = rowsCount;
            ColsCount = colsCount;
        }

        public double this[int row, int column] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int RowsCount { get; }

        public int ColsCount { get; }

        public double ConditionNumber()
        {
            throw new NotImplementedException();
        }

        public double Determinant()
        {
            throw new NotImplementedException();
        }

        public IMatrix<double> Inverse()
        {
            throw new NotImplementedException();
        }

        public double Norm()
        {
            throw new NotImplementedException();
        }
    }
}
