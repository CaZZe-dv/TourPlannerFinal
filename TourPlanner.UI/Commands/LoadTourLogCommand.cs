using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class LoadTourLogCommand : AsyncCommandBase
    {
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly MainMenuViewModel _mainMenuViewModel;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public LoadTourLogCommand(TourPlannerRepository tourPlannerManager, MainMenuViewModel mainMenuViewModel)
        {
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel = mainMenuViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                logger.Info("Loading tour logs...");
                _mainMenuViewModel.IsLoadingTourLogs = true;
                IEnumerable<TourLog> tourLogs = await _tourPlannerManager.GetAllTourLogs(_mainMenuViewModel.SelectedTour.Tour);
                _mainMenuViewModel.UpdateTourLogs(tourLogs);
                _mainMenuViewModel.IsLoadingTourLogs = false;
                logger.Info("Tour logs loaded successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error loading tour logs: {ex.Message}");
                // Handle the exception
            }
        }
    }
}
