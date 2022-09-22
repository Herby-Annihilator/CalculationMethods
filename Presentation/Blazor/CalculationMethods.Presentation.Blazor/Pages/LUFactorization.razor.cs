using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Presentation.Blazor.Infrastructure.Commands;
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
        ISquareMatrix<double> _uMatrix;
        ISquareMatrix<double> _lMatrix;
        ISquareMatrix<double> _matrix;
        IMatrix<double> _reversedMatrix;
        IVector<double> _vectorB;
        IVector<double> _solutionVector;

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
            try
            {
                StateHasChanged();
            }
            catch (Exception e)
            {
                snackbar.Add(e.Message, MudBlazor.Severity.Error);
            }
        }
        private bool CanFindFileCommandExecute(object p) => true;
        #endregion

        #region RestoreSystemCommand
        public ICommand RestoreSystemCommand { get; }
        private void OnRestoreSystemCommandExecuted(object p)
        {
            try
            {
                _matrix = matrixRepository.Get();
                _solutionVector = vectorRepository.Get();
                _vectorB = vectorRepository.Get();
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
                matrixRepository.Save(_matrix);
                vectorRepository.Save(_solutionVector);
                vectorRepository.Save(_vectorB);
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
            _matrix = matrixRepository.Get();
            _uMatrix = matrixRepository.Get();
            _lMatrix = matrixRepository.Get();
            _reversedMatrix = matrixRepository.Get();
            _vectorB = vectorRepository.Get();
            _solutionVector = vectorRepository.Get();
        }
    }
}
