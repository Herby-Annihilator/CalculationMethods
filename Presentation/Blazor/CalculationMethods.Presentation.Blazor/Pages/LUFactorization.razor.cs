using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Dialogs;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Entities.Double;
using CalculationMethods.Infrastructure.Entities.String;
using CalculationMethods.Presentation.Blazor.Infrastructure.Commands;
using CalculationMethods.Presentation.Blazor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Windows.Input;

namespace CalculationMethods.Presentation.Blazor.Pages
{
    public partial class LUFactorization
    {
        private bool _isSolutionVisible = false;
        private int _matrixSize = 0;
        private double _norm;
        private double _conditionNumber;
        private double _determinant;

        private double[,] _templateMatrix = new double[,]
        {
            { 5, 7, 6, 5 },
            { 7, 10, 8, 7 },
            { 6, 8, 10, 9 },
            { 5, 7, 9, 10 }
        };

        private double[] _firstVectorB = new double[] { 23, 32, 33, 31};
        private double[] _secondVectorB = new double[] { 23.01, 31.99, 32.99, 31.01};
        private double[] _thirdVectorB = new double[] { 23.1, 31.9, 32.9, 31.1};

        private ISquareMatrix<double> _uMatrix;
        private ISquareMatrix<double> _lMatrix;
        private ISquareMatrix<double> _matrix;
        private IMatrix<double> _reversedMatrix;
        private IVector<double> _vectorB;
        private IVector<double> _solutionVector;
        private IVector<string> _variablesVector;
        private InputVariant _selectedInputVariant = InputVariant.ReadOnly;
        private InputVariant SelectedInputVariant
        {
            get => _selectedInputVariant;
            set
            {
                _selectedInputVariant = value;
                if (_selectedInputVariant == InputVariant.ReadOnly)
                    _editable = false;
                else
                    _editable = true;
            }
        }
        private bool _editable = false;

        public LUFactorization()
        {
            ClearMatrixCommand = new LambdaCommand(OnClearMatrixCommandExecuted, CanClearMatrixCommandExecute);
            SolveCommand = new LambdaCommand(OnSolveCommandExecuted, CanSolveCommandExecute);
            AcceptMatrixSizeCommand = new LambdaCommand(OnAcceptMatrixSizeCommandExecuted, CanAcceptMatrixSizeCommandExecute);           
            RestoreMatrixCommand = new LambdaCommand(OnRestoreMatrixCommandExecuted, CanRestoreMatrixCommandExecute);
            RestoreVectorCommand = new LambdaCommand(OnRestoreVectorCommandExecuted, CanRestoreVectorCommandExecute);
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
                _conditionNumber = _matrix.ConditionNumber();

                _isSolutionVisible = true;
                
                snackbar.Add("Решение получено", MudBlazor.Severity.Success);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
                StateHasChanged();
            }
        }
        private bool CanSolveCommandExecute(object p) => true;
        #endregion

        #region AcceptMatrixSizeCommand
        public ICommand AcceptMatrixSizeCommand { get; }
        private void OnAcceptMatrixSizeCommandExecuted(object p)
        {
            try
            {
                _matrix = _matrix.ChangeSize(_matrixSize);
                _vectorB = _vectorB.ChangeSize(_matrixSize);
                _solutionVector = _solutionVector.ChangeSize(_matrixSize);
                InitVariablesVector();

                _isSolutionVisible = false;

                StateHasChanged();
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
                StateHasChanged();
            }
        }
        private bool CanAcceptMatrixSizeCommandExecute(object p) => true;
        #endregion

        #region RestoreMatrixCommand
        public ICommand RestoreMatrixCommand { get; }
        private void OnRestoreMatrixCommandExecuted(object p)
        {
            try
            {
                _matrix = FromValues(_templateMatrix);

                snackbar.Add("Матрица восстановлена", MudBlazor.Severity.Info);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
                StateHasChanged();
            }
        }
        private bool CanRestoreMatrixCommandExecute(object p) => true;
        #endregion

        #region RestoreVectorCommand
        public ICommand RestoreVectorCommand { get; }
        private void OnRestoreVectorCommandExecuted(object p)
        {
            try
            {
                if (p is string variant)
                {
                    if (variant == "first")
                    {
                        _vectorB = FromValues(_firstVectorB);
                    }
                    else if (variant == "second")
                    {
                        _vectorB = FromValues(_secondVectorB);
                    }
                    else if (variant == "third")
                    {
                        _vectorB = FromValues(_thirdVectorB);
                    }
                    else
                        throw new ArgumentException($"Fucking command parameter: '{variant}'");
                    snackbar.Add("Вектор восстановлен", MudBlazor.Severity.Info);
                    StateHasChanged();
                }               
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
                StateHasChanged();
            }
        }
        private bool CanRestoreVectorCommandExecute(object p) => true;
        #endregion

        private ISquareMatrix<double> FromValues(double[,] values)
        {
            int rows = values.GetLength(0);
            int cols = values.GetLength(1);
            if (cols != rows)
                throw new ArgumentException($"Число строк: ({rows}) != числу столбцов ({cols})");
            DoubleSquareMatrix result = new DoubleSquareMatrix(rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = values[i, j];
                }
            }
            return result;
        }

        private IVector<double> FromValues(double[] values)
        {
            int length = values.Length;
            DoubleVector result = new DoubleVector(length);
            for (int i = 0; i < length; i++)
            {
                result[i] = values[i];
            }
            return result;
        }

        private void InitVariablesVector()
        {
            _variablesVector = new StringVector(_matrixSize);
            for (int i = 0; i < _matrixSize; i++)
            {
                _variablesVector[i] = $"x{i + 1}";
            }
        }

        protected override void OnInitialized()
        {
            _matrixSize = _templateMatrix.GetLength(0);
            _matrix = FromValues(_templateMatrix);
            _vectorB = FromValues(_firstVectorB);
            _solutionVector = new DoubleVector(_matrixSize);
            InitVariablesVector();
        }

        private double Round(double value) => Math.Round(value, 5);
    }

    public enum InputVariant
    {
        Edit, ReadOnly
    }
}
