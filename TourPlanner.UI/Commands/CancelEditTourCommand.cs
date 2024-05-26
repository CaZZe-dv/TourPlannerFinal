using TourPlanner.UI.Services;

namespace TourPlanner.UI.Commands
{
    public class CancelEditTourCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public CancelEditTourCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
