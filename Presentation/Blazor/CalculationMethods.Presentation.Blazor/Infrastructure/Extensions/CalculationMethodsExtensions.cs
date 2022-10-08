using CalculationMethods.Core.Entities;
using CalculationMethods.Infrastructure.Entities.Double;

namespace CalculationMethods.Presentation.Blazor.Infrastructure.Extensions
{
    public static class CalculationMethodsExtensions
    {
        public static IVector<double> ChangeSize(this IVector<double> oldVector, int size)
        {
            IVector<double> result = new DoubleVector(size);
            if (size > oldVector.Size)
            {
                for (int i = 0; i < oldVector.Size; i++)
                {
                    result[i] = oldVector[i];
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    result[i] = oldVector[i];
                }
            }
            return result;
        }

        public static ISquareMatrix<double> ChangeSize(this ISquareMatrix<double> matrix, int size)
        {
            ISquareMatrix<double> result = new DoubleSquareMatrix(size);
            if (matrix.Size < size)
            {
                for (int i = 0; i < matrix.Size; i++)
                {
                    for (int j = 0; j < matrix.Size; j++)
                    {
                        result[i, j] = matrix[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        result[i, j] = matrix[i, j];
                    }
                }
            }
            return result;
        }

        public static double Max(this IVector<double> vector, Func<double, double> formatValue)
        {
            double max = formatValue(vector[0]);
            double tmp;
            for (int i = 1; i < vector.Size; i++)
            {
                tmp = formatValue(vector[i]);
                if (tmp > max)
                    max = tmp;
            }
            return max;
        }
    }
}
