using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories.Base;
using Microsoft.AspNetCore.Mvc;

namespace CalculationMethods.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatrixController<T> : ControllerBase
    {
        protected IConfiguration configuration;
        protected IFactory<T> factory;

        public MatrixController(IConfiguration configuration, IFactory<T> factory)
        {
            this.configuration = configuration;
            this.factory = factory;
        }

        [HttpGet("/{fileName}")]
        public virtual Task<IMatrix<T>> Get(string fileName)
        {
            return Task.Run(() =>
            {
                return factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateMatrixRepository()
                .Get();
            });
        }

        [HttpPost("/{fileName}")]
        public Task Save(string fileName, [FromBody] IMatrix<T> matrix)
        {
            return Task.Run(() =>
            {
                factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateMatrixRepository()
                .Save(matrix);
            });
        }

        [HttpPut("/{fileName}")]
        public Task Update(string fileName, [FromBody] IMatrix<T> matrix)
        {
            return Task.Run(() =>
            {
                factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateMatrixRepository()
                .Update(matrix);
            });
        }

        [HttpDelete("/{fileName}")]
        public Task Delete(string fileName, [FromBody] IMatrix<T> matrix)
        {
            return Task.Run(() =>
            {
                factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateMatrixRepository()
                .Delete(matrix);
            });
        }

        protected virtual string GetPath(string fileName) => $"{configuration["rootPath"]}{fileName}";
    }
}
