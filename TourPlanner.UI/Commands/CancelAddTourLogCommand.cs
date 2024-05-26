using TourPlanner.UI.Services;

namespace TourPlanner.UI.Commands
{
    public class CancelAddTourLogCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public CancelAddTourLogCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
