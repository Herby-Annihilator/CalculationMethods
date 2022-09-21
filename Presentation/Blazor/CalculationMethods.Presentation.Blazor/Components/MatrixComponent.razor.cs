using CalculationMethods.Core.Entities;
using Microsoft.AspNetCore.Components;

namespace CalculationMethods.Presentation.Blazor.Components
{
    public partial class MatrixComponent<T> 
    {
        [Parameter]
        public IMatrix<T> Matrix { get; set; }
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public bool Editable { get; set; } = false;

        [Parameter]
        public int NameXS { get => _nameXS; set => _nameXS = value; }
        [Parameter]
        public int MatrixXS { get => _matrixXS; set => _matrixXS = value; }

        private int _nameXS;
        private int _matrixXS;

        public MatrixComponent()
        {
            _nameXS = 1;
            _matrixXS = 11;
        }
    }
}
