using CalculationMethods.Core.Entities;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
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
            _matrix = DenseMatrix.Create(RowsCount, ColsCount, 0);
        }

        private Matrix _matrix;

        public double this[int row, int column] { get => _matrix[row, column]; set => _matrix[row, column] = value; }

        public int RowsCount { get; }

        public int ColsCount { get; }

        public double ConditionNumber()
        {
            return _matrix.ConditionNumber();
        }

        public double Determinant()
        {
            return _matrix.Determinant();
        }

        public IMatrix<double> Inverse()
        {
            Matrix<double> inversed = _matrix.Inverse();
            DoubleMatrix result = new DoubleMatrix(RowsCount, ColsCount);
            for (int i = 0; i < inversed.RowCount; i++)
            {
                for (int j = 0; j < inversed.ColumnCount; j++)
                {
                    result[i, j] = inversed[i, j];
                }
            }
            return result;
        }

        public double Norm()
        {
            return _matrix.L1Norm();
        }
    }
}
