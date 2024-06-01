using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class LoadTourCommand : AsyncCommandBase
    {
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly MainMenuViewModel _mainMenuViewModel;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public LoadTourCommand(TourPlannerRepository tourPlannerManager, MainMenuViewModel mainMenuViewModel)
        {
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel = mainMenuViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                logger.Info("Loading tours...");
                _mainMenuViewModel.IsLoadingTours = true;
                IEnumerable<Tour> tours = await _tourPlannerManager.GetAllTours();
                _mainMenuViewModel.UpdateTours(tours);
                _mainMenuViewModel.IsLoadingTours = false;
                logger.Info("Tours loaded successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error loading tours: {ex.Message}");
                // Handle the exception
            }
        }
    }
}
