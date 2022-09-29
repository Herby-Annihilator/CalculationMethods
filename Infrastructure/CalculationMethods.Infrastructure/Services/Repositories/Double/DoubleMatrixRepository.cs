using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Entities.Double;
using CalculationMethods.Infrastructure.Services.Repositories.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.Double
{
    public class DoubleMatrixRepository : IMatrixRepository<IMatrix<double>, double>
    {
        protected string fileName;
        protected MatrixFromFileOptions options;

        public DoubleMatrixRepository(string fileName, MatrixFromFileOptions options)
        {
            this.fileName = fileName;
            this.options = options;
        }

        public bool Delete(IMatrix<double> matrix)
        {
            Guid guid = Guid.NewGuid();
            string tempFile = $"{fileName}_{guid}";
            StreamReader reader = new StreamReader(fileName);
            StreamWriter writer = new StreamWriter(tempFile);
            string buffer;
            while ((buffer = reader.ReadLine()) != null)
            {
                if (buffer == options.OpenTag)
                {
                    do
                    {
                        buffer = reader.ReadLine();
                    } while (buffer != null && buffer != options.CloseTag);
                    if (buffer == null)
                        break;
                    continue;
                }
                writer.WriteLine(buffer);
            }
            writer.Close();
            reader.Close();
            File.Delete(fileName);
            File.Move(tempFile, fileName);
            return true;
        }

        public IMatrix<double> Get()
        {
            DoubleMatrix result;
            if (File.Exists(fileName))
            {
                List<List<double>> listOfRows = new List<List<double>>();
                List<double> row;
                StreamReader reader = new StreamReader(fileName);
                string str;
                string[] digits;
                int minColsCount = int.MaxValue;
                while ((str = reader.ReadLine()) != null)
                {
                    if (str == options.OpenTag)
                    {
                        str = reader.ReadLine();
                        while (str != options.CloseTag && str != null)
                        {
                            digits = str.Split(options.Delimiters, StringSplitOptions.RemoveEmptyEntries);
                            row = new List<double>();
                            if (digits.Length < minColsCount)
                                minColsCount = digits.Length;
                            for (int i = 0; i < digits.Length; i++)
                            {
                                row.Add(Convert.ToDouble(digits[i].Replace(",", ".")));
                            }
                            listOfRows.Add(row);
                            str = reader.ReadLine();
                        }
                        break;
                    }
                }
                result = new DoubleMatrix(listOfRows.Count, minColsCount);
                int rowIndex = 0;
                int colIndex;
                foreach (var item in listOfRows)
                {
                    colIndex = 0;
                    foreach (var digit in item)
                    {
                        if (colIndex < minColsCount)
                        {
                            result[rowIndex, colIndex] = digit;
                            colIndex++;
                        }
                        else
                            break;

                    }
                    rowIndex++;
                }
                reader.Close();
            }
            else
            {
                throw new FileNotFoundException(fileName);
            }
            return result;
        }

        public void Save(IMatrix<double> matrix)
        {
            StringBuilder builder = new StringBuilder();
            string delimeter = options.Delimiters[0] ?? " ";
            builder.Append($"{options.OpenTag}\r\n");
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColsCount; j++)
                {
                    builder.Append($"{matrix[i, j]}{delimeter}");
                }
                builder.Append("\r\n");
            }
            builder.Append($"{options.CloseTag}\r\n");
            File.AppendAllText(fileName, builder.ToString());
        }

        public bool Update(IMatrix<double> matrix)
        {
            Delete(matrix);
            Save(matrix);
            return true;
        }
    }
}
