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

        private int _nameXS;
        private int _matrixXS;

        protected override void OnInitialized()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                _nameXS = 0;
                _matrixXS = 12;
            }
            else
            {
                _nameXS = 1;
                _matrixXS = 11;
            }
            base.OnInitialized();
        }
    }
}
