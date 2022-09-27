using CalculationMethods.Core.Entities;

namespace CalculationMethods.Presentation.Blazor.Models.Interfaces
{
    public interface IEquationSystem<T>
    {
        ISquareMatrix<T> Matrix { get; set; }
        IVector<T> Vector { get; set; }
        IVector<T> SolutionVector { get; set; }
        void Restore();
        void Save();
    }
}
