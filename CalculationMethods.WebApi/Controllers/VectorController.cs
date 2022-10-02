using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories.Base;
using Microsoft.AspNetCore.Mvc;

namespace CalculationMethods.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VectorController<T> : ControllerBase
    {
        private IFactory<T> _factory;
        private IConfiguration _configuration;

        public VectorController(IFactory<T> factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        [HttpGet("/{fileName}")]
        public ActionResult<IVector<T>> Get(string fileName) =>
            Ok(
                _factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateVectorRepository()
                .Get()
            );

        [HttpPost("/{fileName}")]
        public Task Save(string fileName, [FromBody] IVector<T> vector) =>
            Task.Run(() =>
                _factory.FileRepositoryFactory(GetPath(fileName))
                .CreateVectorRepository()
                .Save(vector));

        [HttpPut("/{fileName}")]
        public Task Update(string fileName, [FromBody] IVector<T> vector) =>
            Task.Run(() =>
            Accepted(
                _factory
                .FileRepositoryFactory(GetPath(fileName))
                .CreateVectorRepository()
                .Update(vector)));

        [HttpDelete("/{fileName}")]
        public Task Delete(string fileName, [FromBody] IVector<T> vector) =>
            Task.Run(() =>
            _factory
            .FileRepositoryFactory(GetPath(fileName))
            .CreateVectorRepository()
            .Delete(vector));

        protected virtual string GetPath(string fileName) => $"{_configuration["rootPath"]}{fileName}";
    }
}
