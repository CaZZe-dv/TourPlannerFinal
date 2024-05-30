using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.UI.Views;

namespace TourPlanner.UI.Commands
{
    internal class GetOptionsCommand : ICommand
    {
        private readonly MainMenuViewModel _viewModel;
        private readonly TourReportService _tourReportService;
        private readonly TourReportService tourReportService;


        

        private string tourname;

        public GetOptionsCommand(MainMenuViewModel viewModel)
        {
            _viewModel = viewModel;
            tourReportService = new TourReportService();
            _tourReportService = tourReportService;
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            CustomDialogReport dialog = new CustomDialogReport();
            if (dialog.ShowDialog() == true)
            {
                if (dialog.Tag.ToString() == "TourReport")
                {
                    Debug.WriteLine("einzeln2");
                    if (_viewModel.SelectedTour != null)
                    {
                        string reportFileName = await _tourReportService.GenerateReportForTour(_viewModel.SelectedTour.Tour);
                        //string reportFileName = await _tourReportService.GenerateReportTest(_viewModel.SelectedTour.Tour);
                        Debug.WriteLine($"Report generated for tour: {_viewModel.SelectedTour.Tour.Name}");
                    }
                    else
                    {
                        Debug.WriteLine("No tour selected.");
                    }
                }
                else if (dialog.Tag.ToString() == "SummarizeReport")
                {
                    Debug.WriteLine("alle2");
                    string reportFileName = await _tourReportService.GenerateReportForAllTours();
                    Debug.WriteLine("Report generated for all tours.");
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
