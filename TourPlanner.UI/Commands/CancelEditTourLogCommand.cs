using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class CancelEditTourLogCommand : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationService _navigationService;

        public CancelEditTourLogCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Executing CancelEditTourLogCommand.");
            _navigationService.Navigate();
        }
    }
}
