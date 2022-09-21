using CalculationMethods.Core.Entities;
using Microsoft.AspNetCore.Components;

namespace CalculationMethods.Presentation.Blazor.Components
{
    public partial class VectorComponent<T>
    {
        [Parameter]
        public DisplayMode Mode { get; set; } = DisplayMode.Horizontal;

        [Parameter]
        public IVector<T> Vector { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public bool Editable { get; set; } = false;

        [Parameter]
        public int NameXS { get; set; }

        [Parameter]
        public int VectorXS { get; set; }
    }

    public enum DisplayMode
    {
        Vertical, Horizontal
    }
}
