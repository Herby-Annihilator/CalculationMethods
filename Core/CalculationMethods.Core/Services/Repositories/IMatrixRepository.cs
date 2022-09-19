using CalculationMethods.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services.Repositories
{
    public interface IMatrixRepository<TMatrix, TElement> 
        where TMatrix : IMatrix<TElement>
        where TElement : IComparable, IComparable<TElement>, IConvertible, IEquatable<TElement>, ISpanFormattable
    {
        void Save(TMatrix matrix);
        TMatrix Get();
        bool Update(TMatrix matrix);
        bool Delete(TMatrix matrix);
    }
}
