using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Presentation.Blazor.Models.Interfaces;

namespace CalculationMethods.Presentation.Blazor.Models
{
    public class EquationSystem<T> : IEquationSystem<T>
    {
        private IConfiguration _configuration;
        private IFactory<T> _factory;

        public EquationSystem(IConfiguration configuration, IFactory<T> factory)
        {
            _configuration = configuration;
            _factory = factory;
            _matrixChanged = false;
            _vectorChanged = false;
            _solutionVectorChanged = false;
        }
        private ISquareMatrix<T> _matrix;
        private bool _matrixChanged;
        public ISquareMatrix<T> Matrix 
        { 
            get => _matrix;
            set
            {
                _matrix = value;
                _matrixChanged = true;
            }
        }

        private IVector<T> _vector;
        private bool _vectorChanged;
        public IVector<T> Vector
        {
            get => _vector;
            set
            {
                _vector = value;
                _vectorChanged = true;
            }
        }

        private IVector<T> _solutionVector;
        private bool _solutionVectorChanged;
        public IVector<T> SolutionVector
        {
            get => _solutionVector;
            set
            {
                _solutionVector = value;
                _solutionVectorChanged = true;
            }
        }
        public void Restore()
        {
            string matrixFileName = _configuration["matrix"];
            string solutionFileName = _configuration["solutionVector"];
            string vectorFileName = _configuration["vector"];
            Matrix = _factory.FileRepositoryFactory(matrixFileName).CreateSquareMatrixRepository().Get();
            Vector = _factory.FileRepositoryFactory(vectorFileName).CreateVectorRepository().Get();
            SolutionVector = _factory.FileRepositoryFactory(solutionFileName).CreateVectorRepository().Get();
            _matrixChanged = false;
            _solutionVectorChanged = false;
            _vectorChanged = false;
        }

        public void Save()
        {
            string matrixFileName = _configuration["matrix"];
            string solutionFileName = _configuration["solutionVector"];
            string vectorFileName = _configuration["vector"];
            if (_matrixChanged)
            {
                _factory.FileRepositoryFactory(matrixFileName).CreateMatrixRepository().Save(Matrix);
                _matrixChanged = false;
            }
                
            if (_vectorChanged)
            {
                _factory.FileRepositoryFactory(vectorFileName).CreateVectorRepository().Save(Vector);
                _vectorChanged = false;
            }
            if (_solutionVectorChanged)
            {
                _factory.FileRepositoryFactory(solutionFileName).CreateVectorRepository().Save(SolutionVector);
                _solutionVectorChanged = false;
            }
        }
    }
}
