using CalculationMethods.Core.Entities;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Entities.Double
{
    public class DoubleVector : IVector<double>
    {
        protected Vector _vector;
        public int Size { get; }
        public double this[int index] { get => _vector[index]; set => _vector[index] = value; }

        public DoubleVector(int size)
        {
            Size = size;
        }
    }
}
