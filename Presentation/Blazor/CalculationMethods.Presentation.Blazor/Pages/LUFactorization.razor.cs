﻿using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Dialogs;
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

        [Inject]
        protected IFactory<double> RepositoryFactory { get; set; }
        [Inject]
        protected IConfiguration Configuration { get; set; }

        public LUFactorization()
        {
            ClearMatrixCommand = new LambdaCommand(OnClearMatrixCommandExecuted, CanClearMatrixCommandExecute);
            SolveCommand = new LambdaCommand(OnSolveCommandExecuted, CanSolveCommandExecute);
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


        #region RestoreSystemCommand
        public ICommand RestoreSystemCommand { get; }
        private void OnRestoreSystemCommandExecuted(object p)
        {
            try
            {
                string _matrixFileName = Path.Combine(Environment.CurrentDirectory, Configuration["matrix"]);
                string _vectorFileName = Path.Combine(Environment.CurrentDirectory, Configuration["vector"]);
                string _solutionVectorFileName = Path.Combine(Environment.CurrentDirectory, Configuration["solutionVector"]);
                _matrix = RepositoryFactory.FileRepositoryFactory(_matrixFileName).CreateSquareMatrixRepository().Get();
                _solutionVector = RepositoryFactory.FileRepositoryFactory(_solutionVectorFileName).CreateVectorRepository().Get();
                _vectorB = RepositoryFactory.FileRepositoryFactory(_vectorFileName).CreateVectorRepository().Get();
                
                snackbar.Add("Система восстановлена", MudBlazor.Severity.Info);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
                StateHasChanged();
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
                string _matrixFileName = Path.Combine(Environment.CurrentDirectory, Configuration["matrix"]);
                string _vectorFileName = Path.Combine(Environment.CurrentDirectory, Configuration["vector"]);
                string _solutionVectorFileName = Path.Combine(Environment.CurrentDirectory, Configuration["solutionVector"]);
                RepositoryFactory.FileRepositoryFactory(_matrixFileName).CreateSquareMatrixRepository().Save(_matrix);
                RepositoryFactory.FileRepositoryFactory(_solutionVectorFileName).CreateVectorRepository().Save(_solutionVector);
                RepositoryFactory.FileRepositoryFactory(_vectorFileName).CreateVectorRepository().Save(_vectorB);
                
                snackbar.Add("Система сохранена", MudBlazor.Severity.Info);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, MudBlazor.Severity.Error);
                StateHasChanged();
            }
        }
        private bool CanSaveSystemCommandExecute(object p) => true;
        #endregion
    }
}
