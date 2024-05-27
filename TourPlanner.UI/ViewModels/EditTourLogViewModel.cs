using System.Windows.Input;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;

namespace TourPlanner.UI.ViewModels
{
    public class EditTourLogViewModel : ViewModelBase
    {
        private DateTime _editTourLogDateTime;
        public DateTime EditTourLogDateTime
        {
            get => _editTourLogDateTime;
            set
            {
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
                _editTourLogRating = value;
                OnPropertyChanged(nameof(EditTourLogRating));
            }
        }
        public ICommand UpdateEditTourLogCommand { get; }
        public ICommand CancelEditTourLogCommand { get; }
        public EditTourLogViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            TourLog existingTourLog = sharedDataService.SelectedTourLog;

            _editTourLogDateTime = existingTourLog.DateTime;
            _editTourLogComment = existingTourLog.Comment;
            _editTourLogDifficulty = existingTourLog.Difficulty;
            _editTourLogRating = existingTourLog.Rating;
            _editTourLogTotalDistance = existingTourLog.TotalDistance;
            _editTourLogTotalTime = existingTourLog.TotalTime;

            UpdateEditTourLogCommand = new UpdateEditTourLogCommand(tourPlannerManager, navigationService, this, sharedDataService);
            CancelEditTourLogCommand = new CancelEditTourLogCommand(navigationService);
        }
    }
}
