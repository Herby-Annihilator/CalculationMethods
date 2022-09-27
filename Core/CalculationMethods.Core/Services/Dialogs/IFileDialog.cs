using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Core.Services.Dialogs
{
    public interface IFileDialog
    {
        public string SelectedFileName { get; set; }
        public string SelectedFileFullName { get; set; }
        public bool Multiselection { get; set; }
        public string Title { get; set; }
        public bool CheckFileExists { get; set; }
        public bool CheckPathExists { get; set; }
        public string InitialDirectory { get; set; }
        public string[] FileNames { get; }

        public bool ShowDialog();
    }
}
