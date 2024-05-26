using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class LoadTourLogCommand : AsyncCommandBase
    {
        private readonly TourPlannerManager _tourPlannerManager;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public LoadTourLogCommand(TourPlannerManager tourPlannerManager, MainMenuViewModel mainMenuViewModel)
        {
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel = mainMenuViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _mainMenuViewModel.IsLoadingTourLogs = true;
            IEnumerable<TourLog> tourLogs = await _tourPlannerManager.GetAllTourLogs(_mainMenuViewModel.SelectedTour.Tour);
            _mainMenuViewModel.UpdateTourLogs(tourLogs);
            _mainMenuViewModel.IsLoadingTourLogs = false;
        }
    }
}
