using TourPlanner.UI.Services;

namespace TourPlanner.UI.Commands
{
    public class AddTourCommand : CommandBase
    {
        private readonly NavigationService _addTourViewNavigationService;
        public AddTourCommand(NavigationService addTourViewNavigationService)
        {
            _addTourViewNavigationService = addTourViewNavigationService;
        }
        public override void Execute(object? parameter)
        {

            _addTourViewNavigationService.Navigate();
        }
    }
}
