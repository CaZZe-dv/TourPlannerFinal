using System;
using TourPlanner.BL.Models;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class TourLogViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public readonly TourLog TourLog;
        public DateTime DateTime => TourLog.DateTime;
        public string Comment => TourLog.Comment;
        public float Difficulty => TourLog.Difficulty;
        public float TotalDistance => TourLog.TotalDistance;
        public TimeSpan TotalTime => TourLog.TotalTime;
        public float Rating => TourLog.Rating;

        public TourLogViewModel(TourLog tourLog)
        {
            logger.Info("Creating TourLogViewModel.");
            TourLog = tourLog;
            logger.Info("TourLogViewModel created.");
        }
    }
}
