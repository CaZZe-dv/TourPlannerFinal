using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    internal class GetOptionsCommand : ICommand
    {
        private readonly MainMenuViewModel _viewModel;

        public GetOptionsCommand(MainMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            // Display a simple MessageBox to choose the option
            var result = MessageBox.Show("Choose an option:\n\n1. Generate report for selected tour\n2. Generate report for all tours",
                "Generate Report", MessageBoxButton.YesNoCancel);

            // Yes corresponds to option 1
            if (result == MessageBoxResult.Yes)
            {
                _viewModel.GenerateReportForSelectedTour();
            }
            // No corresponds to option 2
            else if (result == MessageBoxResult.No)
            {
                _viewModel.GenerateReportForAllTours();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
