using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class CancelEditTourCommand : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationService _navigationService;

        public CancelEditTourCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Executing CancelEditTourCommand.");
            _navigationService.Navigate();
        }
    }
}
