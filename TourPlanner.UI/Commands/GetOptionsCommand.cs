using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL.Models;
using TourPlanner.BL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.UI.Views;
using TourPlanner.DAL.Services;

namespace TourPlanner.UI.Commands
{
    internal class GetOptionsCommand : ICommand
    {
        private readonly MainMenuViewModel _viewModel;
        private readonly TourPlannerRepository _tourPlannerManger;
        private readonly TourReportService _tourReportService;
        private readonly TourReportService tourReportService;
        private readonly DbTourReportService dbTourReportService;
        private readonly DbTourReportService _dbTourReportService;


        public GetOptionsCommand(MainMenuViewModel viewModel, TourPlannerRepository tourPlannerManager)
        {
            _viewModel = viewModel;
            _tourPlannerManger = tourPlannerManager;
            tourReportService = new TourReportService();
            _tourReportService = tourReportService;
            dbTourReportService = new DbTourReportService();
            _dbTourReportService = dbTourReportService;
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            CustomDialogReport dialog = new CustomDialogReport();
            if (dialog.ShowDialog() == true)
            {
                if (dialog.Tag.ToString() == "TourReport")
                {
                    Debug.WriteLine("einzelnReport");
                    if (_viewModel.SelectedTour != null)
                    {
                        string reportFileName = await _dbTourReportService.GenerateReportForTour(_viewModel.SelectedTour.Tour, _tourPlannerManger);
                        //string reportFileName = await _tourReportService.GenerateReportTest(_viewModel.SelectedTour.Tour);
                        Debug.WriteLine($"Report generated for tour: {_viewModel.SelectedTour.Tour.Name}");
                        //_viewModel.GenerateReportForSelectedTour;
                    }
                    else
                    {
                        Debug.WriteLine("No tour selected.");
                    }
                }
                else if (dialog.Tag.ToString() == "SummarizeReport")
                {
                    Debug.WriteLine("alleReport");
                    string reportFileName = await _dbTourReportService.GenerateReportForAllTours(_tourPlannerManger);
                    Debug.WriteLine("Report generated for all tours.");
                    //_viewModel.GenerateReportForAllTours;
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
