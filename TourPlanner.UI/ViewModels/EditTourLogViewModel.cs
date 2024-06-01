using System.Windows.Input;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class EditTourLogViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private DateTime _editTourLogDateTime;
        public DateTime EditTourLogDateTime
        {
            get => _editTourLogDateTime;
            set
            {
                logger.Info($"EditTourLogDateTime changed from '{_editTourLogDateTime}' to '{value}'");
                _editTourLogDateTime = value;
                OnPropertyChanged(nameof(EditTourLogDateTime));
            }
        }

        private string _editTourLogComment;
        public string EditTourLogComment
        {
            get => _editTourLogComment;
            set
            {
                logger.Info($"EditTourLogComment changed from '{_editTourLogComment}' to '{value}'");
                _editTourLogComment = value;
                OnPropertyChanged(nameof(EditTourLogComment));
            }
        }

        private float _editTourLogDifficulty;
        public float EditTourLogDifficulty
        {
            get => _editTourLogDifficulty;
            set
            {
                logger.Info($"EditTourLogDifficulty changed from '{_editTourLogDifficulty}' to '{value}'");
                _editTourLogDifficulty = value;
                OnPropertyChanged(nameof(EditTourLogDifficulty));
            }
        }

        private float _editTourLogTotalDistance;
        public float EditTourLogTotalDistance
        {
            get => _editTourLogTotalDistance;
            set
            {
                logger.Info($"EditTourLogTotalDistance changed from '{_editTourLogTotalDistance}' to '{value}'");
                _editTourLogTotalDistance = value;
                OnPropertyChanged(nameof(EditTourLogTotalDistance));
            }
        }

        private TimeSpan _editTourLogTotalTime;
        public TimeSpan EditTourLogTotalTime
        {
            get => _editTourLogTotalTime;
            set
            {
                logger.Info($"EditTourLogTotalTime changed from '{_editTourLogTotalTime}' to '{value}'");
                _editTourLogTotalTime = value;
                OnPropertyChanged(nameof(EditTourLogTotalTime));
            }
        }

        private float _editTourLogRating;
        public float EditTourLogRating
        {
            get => _editTourLogRating;
            set
            {
                logger.Info($"EditTourLogRating changed from '{_editTourLogRating}' to '{value}'");
                _editTourLogRating = value;
                OnPropertyChanged(nameof(EditTourLogRating));
            }
        }

        public ICommand UpdateEditTourLogCommand { get; }
        public ICommand CancelEditTourLogCommand { get; }

        public EditTourLogViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            logger.Info("Initializing EditTourLogViewModel.");

            TourLog existingTourLog = sharedDataService.SelectedTourLog;

            _editTourLogDateTime = existingTourLog.DateTime;
            _editTourLogComment = existingTourLog.Comment;
            _editTourLogDifficulty = existingTourLog.Difficulty;
            _editTourLogRating = existingTourLog.Rating;
            _editTourLogTotalDistance = existingTourLog.TotalDistance;
            _editTourLogTotalTime = existingTourLog.TotalTime;

            logger.Info("EditTourLogViewModel initialized with existing tour log data.");

            UpdateEditTourLogCommand = new UpdateEditTourLogCommand(tourPlannerManager, navigationService, this, sharedDataService);
            CancelEditTourLogCommand = new CancelEditTourLogCommand(navigationService);
        }
    }
}
