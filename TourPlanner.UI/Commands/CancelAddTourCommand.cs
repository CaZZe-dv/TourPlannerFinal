using TourPlanner.UI.Services;

namespace TourPlanner.UI.Commands
{
    public class CancelAddTourCommand : CommandBase
    {
        private readonly NavigationService _navigationService;
        public CancelAddTourCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
