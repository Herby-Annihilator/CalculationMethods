using CalculationMethods.Core.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.AspNetCore.Components.Forms;

namespace CalculationMethods.Infrastructure.Services.Dialogs
{
    public class FileDialog : IFileDialog
    {
        public string SelectedFileName { get; set; } = "";
        public string SelectedFileFullName { get; set; } = "";
        public bool Multiselection { get; set; } = false;
        public string Title { get; set; } = "";
        public bool CheckFileExists { get; set; } = true;
        public bool CheckPathExists { get; set; } = true;
        public string InitialDirectory { get; set; } = Environment.CurrentDirectory;
        public string[] FileNames { get; private set; } = new string[0];

        public bool ShowDialog()
        {
            //InputFile inputFile = new InputFile();
            //OpenFileDialog dialog = new OpenFileDialog
            //{
            //    Multiselect = Multiselection,
            //    Title = Title,
            //    CheckFileExists = CheckFileExists,
            //    CheckPathExists = CheckPathExists,
            //    InitialDirectory = InitialDirectory,
            //};
            //if (dialog.ShowDialog() == true)
            //{
            //    FileNames = dialog.FileNames;
            //    SelectedFileFullName = FileNames[0];
            //    SelectedFileName = SelectedFileFullName.Substring(SelectedFileFullName.LastIndexOf("\\") + 1);
            //    return true;
            //}
            return false;
        }
    }
}
