using TourPlanner.UI.Services;

namespace TourPlanner.UI.Commands
{
    public class CancelEditTourLogCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public CancelEditTourLogCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
