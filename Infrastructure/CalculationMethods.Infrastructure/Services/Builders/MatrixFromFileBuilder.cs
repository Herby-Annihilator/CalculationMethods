using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services;
using CalculationMethods.Infrastructure.Entities.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Builders
{
    public class MatrixFromFileBuilder : IMatrixBuilder<double>
    {
        protected MatrixFromFileBuilderOptions options;
        protected string fileName;
        public MatrixFromFileBuilder(string fileName, MatrixFromFileBuilderOptions options)
        {
            this.fileName = fileName;
            this.options = options;
        }
        public IMatrix<double> Build()
        {
            DoubleSquareMatrix result;
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
                result = new DoubleSquareMatrix(listOfRows.Count);
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
    }

    public class MatrixFromFileBuilderOptions
    {
        public string OpenTag { get; set; }
        public string CloseTag { get; set; }
        public string[] Delimiters { get; set; }
    }
}
