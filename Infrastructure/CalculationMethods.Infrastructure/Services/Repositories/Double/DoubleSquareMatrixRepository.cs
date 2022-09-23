using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Entities.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Double
{
    public class DoubleSquareMatrixRepository : IMatrixRepository<ISquareMatrix<double>, double>
    {
        protected string fileName;
        protected MatrixFromFileOptions options;
        private DoubleSquareMatrixRepository _repository;

        public DoubleSquareMatrixRepository(string fileName, MatrixFromFileOptions options)
        {
            this.fileName = fileName;
            this.options = options;
            _repository = new DoubleSquareMatrixRepository(fileName, options);
        }

        public bool Delete(ISquareMatrix<double> matrix)
        {
            return _repository.Delete(matrix);
        }

        public ISquareMatrix<double> Get()
        {
            ISquareMatrix<double> result;
            IMatrix<double> matrix = _repository.Get();
            if (matrix.ColsCount == matrix.RowsCount)
            {
                result = new DoubleSquareMatrix(matrix.ColsCount);
                for (int i = 0; i < matrix.RowsCount; i++)
                {
                    for (int j = 0; j < matrix.ColsCount; j++)
                    {
                        result[i, j] = matrix[i, j];
                    }
                }
                return result;
            }
            else
                throw new InvalidOperationException("Matrix in file is not square matrix!");
        }

        public void Save(ISquareMatrix<double> matrix) => _repository.Save(matrix);

        public bool Update(ISquareMatrix<double> matrix) => _repository.Update(matrix);
    }

    public class MatrixFromFileOptions
    {
        public string OpenTag { get; set; }
        public string CloseTag { get; set; }
        public string[] Delimiters { get; set; }
    }
}
