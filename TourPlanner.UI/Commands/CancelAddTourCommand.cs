using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class CancelAddTourCommand : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationService _navigationService;

        public CancelAddTourCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Executing CancelAddTourCommand.");
            _navigationService.Navigate();
        }
    }
}
