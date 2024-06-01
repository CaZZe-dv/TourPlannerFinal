using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class AddTourCommand : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationService _addTourViewNavigationService;

        public AddTourCommand(NavigationService addTourViewNavigationService)
        {
            _addTourViewNavigationService = addTourViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Executing AddTourCommand.");
            _addTourViewNavigationService.Navigate();
        }
    }
}
