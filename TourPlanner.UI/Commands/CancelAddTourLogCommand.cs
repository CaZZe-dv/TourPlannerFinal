using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class CancelAddTourLogCommand : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationService _navigationService;

        public CancelAddTourLogCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Executing CancelAddTourLogCommand.");
            _navigationService.Navigate();
        }
    }
}
