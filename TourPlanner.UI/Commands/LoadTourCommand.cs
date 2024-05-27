using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class LoadTourCommand : AsyncCommandBase
    {
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public LoadTourCommand(TourPlannerRepository tourPlannerManager, MainMenuViewModel mainMenuViewModel)
        {
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel = mainMenuViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _mainMenuViewModel.IsLoadingTours = true;
            IEnumerable<Tour> tours = await _tourPlannerManager.GetAllTours();
            _mainMenuViewModel.UpdateTours(tours);
            _mainMenuViewModel.IsLoadingTours = false;
        }
    }
}
