using TourPlanner.BL.Models;

namespace TourPlanner.UI.ViewModels
{
    public class TourLogViewModel : ViewModelBase
    {
        public readonly TourLog TourLog;
        public DateTime DateTime => TourLog.DateTime;
        public string Comment => TourLog.Comment;
        public float Difficulty => TourLog.Difficulty;
        public float TotalDistance => TourLog.TotalDistance;
        public TimeSpan TotalTime => TourLog.TotalTime;
        public float Rating => TourLog.Rating;
        public TourLogViewModel(TourLog tourLog)
        {
            TourLog = tourLog;
        }
    }
}
