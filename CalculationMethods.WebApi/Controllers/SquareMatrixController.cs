using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories.Base;
using Microsoft.AspNetCore.Mvc;

namespace CalculationMethods.WebApi.Controllers
{
    public class SquareMatrixController<T> : MatrixController<T>
    {
        public SquareMatrixController(IConfiguration configuration, IFactory<T> factory) : base(configuration, factory)
        {
        }

        [HttpGet("/{fileName}")]
        public override Task<IMatrix<T>> Get(string fileName)
        {
            return Task.Run(() =>
            {
                return (IMatrix<T>)factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateSquareMatrixRepository()
                .Get();
            });
        }
    }
}
