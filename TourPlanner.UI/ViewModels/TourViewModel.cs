using TourPlanner.BL.Models;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public readonly Tour Tour;
        public string Name => Tour.Name;

        public TourViewModel(Tour tour)
        {
            logger.Info("Creating TourViewModel.");
            Tour = tour;
            logger.Info("TourViewModel created.");
        }
    }
}
