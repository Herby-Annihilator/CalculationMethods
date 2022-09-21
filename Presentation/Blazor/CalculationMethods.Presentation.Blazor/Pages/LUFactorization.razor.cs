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
        ISquareMatrix<double> _reversedMatrix;
        IVector<double> _vectorB;
        IVector<double> _vector;

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
        private bool CanSolveCommandExecute(object p) => true;
        #endregion

        #region FindFileCommand
        public ICommand FindFileCommand { get; }
        private void OnFindFileCommandExecuted(object p)
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
        private bool CanFindFileCommandExecute(object p) => true;
        #endregion

        #region RestoreSystemCommand
        public ICommand RestoreSystemCommand { get; }
        private void OnRestoreSystemCommandExecuted(object p)
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
        private bool CanRestoreSystemCommandExecute(object p) => true;
        #endregion

        #region SaveSystemCommand
        public ICommand SaveSystemCommand { get; }
        private void OnSaveSystemCommandExecuted(object p)
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
        private bool CanSaveSystemCommandExecute(object p) => true;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            _matrix = matrixRepository.Get();
            _uMatrix = matrixRepository.Get();
            _lMatrix = matrixRepository.Get();
            _reversedMatrix = matrixRepository.Get();
            _vectorB = vectorRepository.Get();
            _vector = vectorRepository.Get();
        }
    }
}
