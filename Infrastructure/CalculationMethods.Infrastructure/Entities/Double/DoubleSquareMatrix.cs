using CalculationMethods.Core.Entities;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Entities.Double
{
    public class DoubleSquareMatrix : ISquareMatrix<double>
    {
        private Matrix _matrix;
        private Matrix _uMatrix;
        private Matrix _lMatrix;
        private int _size;
        public int Size => _size;

        public int RowsCount => Size;

        public int ColsCount => Size;

        protected bool _matrixChanged = false;

        public double this[int row, int column] 
        { 
            get => _matrix[row, column];
            set
            {
                if (_matrix[row, column] != value)
                    _matrixChanged = true;
                _matrix[row, column] = value;
            }
        }

        public DoubleSquareMatrix(int size)
        {
            _size = size;
            _matrix = DenseMatrix.Create(Size, Size, 0);
            _matrixChanged = true;
        }
        public ISquareMatrix<double> GetLMatrix()
        {
            if (_matrixChanged)
                GetLUFactorization();
            return FromMatrix(_lMatrix);
        }

        public ISquareMatrix<double> GetUMatrix()
        {
            if (_matrixChanged)
                GetLUFactorization();
            return FromMatrix(_uMatrix);
        }

        

        protected virtual void GetLUFactorization()
        {
            CheckMatrixForFactorization();
            if (_matrixChanged)
            {
                _uMatrix = DenseMatrix.Create(Size, Size, 0);
                _lMatrix = DenseMatrix.CreateIdentity(Size);
                _matrixChanged = false;
            }
            double sum;
            for (int i = 0; i < Size; i++)
            {
                
                for (int j = 0; j < Size; j++)
                {
                    sum = 0;
                    if (i <= j)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            sum += _lMatrix[i, k] * _uMatrix[k, j];
                        }
                        _uMatrix[i, j] = _matrix[i, j] - sum;
                    }
                    else
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += _lMatrix[i, k] * _uMatrix[k, j];
                        }
                        _lMatrix[i, j] = (_matrix[i, j] - sum) / _uMatrix[j, j];
                    }
                }
            }
        }

        protected virtual void CheckMatrixForFactorization()
        {
            if (!HasLUFactorization())
                throw new InvalidOperationException("Matrix has no LU-factorization, because element matrix[0, 0] is zero");
        }
        protected virtual bool HasLUFactorization() => _matrix[0, 0] != 0;

        public virtual double Determinant()
        {
            if (_matrixChanged)
                GetLUFactorization();
            return Determinant(_lMatrix) * Determinant(_uMatrix);
        }

        protected double Determinant(Matrix matrix)
        {
            double result = 1;
            for (int i = 0; i < Size; i++)
            {
                result *= matrix[i, i];
            }
            return result;
        }

        public virtual IMatrix<double> Inverse()
        {
            if (_matrixChanged)
                GetLUFactorization();
            DoubleSquareMatrix x = new DoubleSquareMatrix(Size);
            DoubleVector b = new DoubleVector(Size);
            IVector<double> tmp;
            for (int i = 0; i < Size; i++)
            {
                b[i] = 1;
                tmp = Solve(b);
                for (int j = 0; j < Size; j++)
                {
                    x[j, i] = tmp[j];
                }
                b[i] = 0;
            }
            return x;
        }

        public virtual IVector<double> Solve(IVector<double> b)
        {
            if (_matrixChanged)
                GetLUFactorization();
            IVector<double> y = new DoubleVector(Size);
            double sum;
            for (int i = 0; i < Size; i++)
            {
                sum = 0;
                for (int k = 0; k < i; k++)
                {
                    sum += _lMatrix[i, k] * y[k];
                }
                y[i] = b[i] - sum;
            }
            IVector<double> x = new DoubleVector(Size);
            for (int i = Size - 1; i >= 0; i--)
            {
                sum = 0;
                for (int k = i + 1; k < Size; k++)
                {
                    sum += _uMatrix[i, k] * x[k];
                }
                x[i] = 1 / _uMatrix[i, i] * (y[i] - sum);
            }
            return x;
        }

        public virtual double Norm()
        {
            double max = -1;
            double tmp;
            for (int i = 0; i < Size; i++)
            {
                tmp = 0;
                for (int j = 0; j < Size; j++)
                {
                    tmp += Math.Abs(_matrix[i, j]);
                }
                if (tmp > max)
                    max = tmp;
            }
            return max;
        }

        public double ConditionNumber() => Norm() * Inverse().Norm();

        protected virtual DoubleSquareMatrix FromMatrix(Matrix matrix)
        {
            if (matrix.RowCount != Size || matrix.ColumnCount != Size)
                throw new ArgumentException("Values of matrix.RowCount, matrix.ColumnCount and Size are not equal");
            DoubleSquareMatrix result = new DoubleSquareMatrix(Size);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }
            return result;
        }
    }
}
