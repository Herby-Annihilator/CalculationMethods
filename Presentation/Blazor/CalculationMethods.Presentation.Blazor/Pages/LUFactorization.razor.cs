using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Presentation.Blazor.Infrastructure.Commands;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Windows.Input;

namespace CalculationMethods.Presentation.Blazor.Pages
{
    public partial class LUFactorization
    {
        private bool _isSolutionVisible = false;
        private string _path;
        private int _matrixSize = 0;
        private double _norm;
        private double _conditionNumber;
        private double _determinant;
        private ISquareMatrix<double> _uMatrix;
        private ISquareMatrix<double> _lMatrix;
        private ISquareMatrix<double> _matrix;
        private IMatrix<double> _reversedMatrix;
        private IVector<double> _vectorB;
        private IVector<double> _solutionVector;

        protected IMatrixRepository<ISquareMatrix<double>, double> _matrixRepository;
        protected IMatrixRepository<ISquareMatrix<double>, double> MatrixRepository => 
            _matrixRepository ??= RepositoryFactory.MockRepositoryFactory().CreateSquareMatrixRepository();

        protected IVectorRepository<double> _vectorRepository;
        protected IVectorRepository<double> VectorRepository =>
            _vectorRepository ??= RepositoryFactory.MockRepositoryFactory().CreateVectorRepository();

        [Inject]
        protected IFactory<double> RepositoryFactory { get; set; }

        public LUFactorization()
        {
            ClearMatrixCommand = new LambdaCommand(OnClearMatrixCommandExecuted, CanClearMatrixCommandExecute);
            SolveCommand = new LambdaCommand(OnSolveCommandExecuted, CanSolveCommandExecute);
            FindFileCommand = new LambdaCommand(OnFindFileCommandExecuted, CanFindFileCommandExecute);
            RestoreSystemCommand = new LambdaCommand(OnRestoreSystemCommandExecuted, CanRestoreSystemCommandExecute);
            SaveSystemCommand = new LambdaCommand(OnSaveSystemCommandExecuted, CanSaveSystemCommandExecute);
        }

        #region ClearMatrixCommand
        public ICommand ClearMatrixCommand { get; }
        private void OnClearMatrixCommandExecuted(object p)
        {
            for (int i = 0; i < _matrix.RowsCount; i++)
            {
                for (int j = 0; j < _matrix.ColsCount; j++)
                {
                    _matrix[i, j] = 0;
                }
            }
            _isSolutionVisible = false;
            StateHasChanged();
        }
        private bool CanClearMatrixCommandExecute(object p) => true;
        #endregion

        #region SolveCommand
        public ICommand SolveCommand { get; }
        private void OnSolveCommandExecuted(object p)
        {
            try
            {
                _solutionVector = _matrix.Solve(_vectorB);
                _uMatrix = _matrix.GetUMatrix();
                _lMatrix = _matrix.GetLMatrix();
                _determinant = _matrix.Determinant();
                _norm = _matrix.Norm();
                _reversedMatrix = _matrix.Inverse();

                _isSolutionVisible = true;
                StateHasChanged();
                snackbar.Add("Решение получено", MudBlazor.Severity.Success);
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
            }
        }
        private bool CanSolveCommandExecute(object p) => true;
        #endregion

        #region FindFileCommand
        public ICommand FindFileCommand { get; }
        private void OnFindFileCommandExecuted(object p)
        {
            
        }
        private bool CanFindFileCommandExecute(object p) => true;

        private void UploadFile(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.File;
                _path = file.Name;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
            }
        }
        #endregion

        #region RestoreSystemCommand
        public ICommand RestoreSystemCommand { get; }
        private void OnRestoreSystemCommandExecuted(object p)
        {
            try
            {
                _matrix = MatrixRepository.Get();
                _solutionVector = VectorRepository.Get();
                _vectorB = VectorRepository.Get();
                StateHasChanged();
                snackbar.Add("Система восстановлена", MudBlazor.Severity.Info);
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
            }
        }
        private bool CanRestoreSystemCommandExecute(object p) => true;
        #endregion

        #region SaveSystemCommand
        public ICommand SaveSystemCommand { get; }
        private void OnSaveSystemCommandExecuted(object p)
        {
            try
            {
                MatrixRepository.Save(_matrix);
                VectorRepository.Save(_solutionVector);
                VectorRepository.Save(_vectorB);
                StateHasChanged();
                snackbar.Add("Система сохранена", MudBlazor.Severity.Info);
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
            }
        }
        private bool CanSaveSystemCommandExecute(object p) => true;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            _matrix = MatrixRepository.Get();
            _uMatrix = MatrixRepository.Get();
            _lMatrix = MatrixRepository.Get();
            _reversedMatrix = MatrixRepository.Get();
            _vectorB = VectorRepository.Get();
            _solutionVector = VectorRepository.Get();
        }
    }
}
