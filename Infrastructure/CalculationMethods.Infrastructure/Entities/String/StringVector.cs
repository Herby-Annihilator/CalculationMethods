using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Entities.String
{
    public class StringVector : IVector<string>
    {
        protected string[] strings;
        public string this[int index] { get => strings[index]; set => strings[index] = value; }

        public int Size => strings.Length;
        public StringVector(int size)
        {
            strings = new string[size];
        }
    }
}
