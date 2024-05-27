using System.Windows.Input;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;

namespace TourPlanner.UI.ViewModels
{
    public class AddTourLogViewModel : ViewModelBase
    {

        private DateTime _addTourLogDateTime;
        public DateTime AddTourLogDateTime
        {
            get => _addTourLogDateTime;
            set
            {
                _addTourLogDateTime = value;
                OnPropertyChanged(nameof(AddTourLogDateTime));
            }
        }

        private string _addTourLogComment;
        public string AddTourLogComment
        {
            get => _addTourLogComment;
            set
            {
                _addTourLogComment = value;
                OnPropertyChanged(nameof(AddTourLogComment));
            }
        }

        private float _addTourLogDifficulty;
        public float AddTourLogDifficulty
        {
            get => _addTourLogDifficulty;
            set
            {
                _addTourLogDifficulty = value;
                OnPropertyChanged(nameof(AddTourLogDifficulty));
            }
        }

        private float _addTourLogTotalDistance;
        public float AddTourLogTotalDistance
        {
            get => _addTourLogTotalDistance;
            set
            {
                _addTourLogTotalDistance = value;
                OnPropertyChanged(nameof(AddTourLogTotalDistance));
            }
        }

        private TimeSpan _addTourLogTotalTime;
        public TimeSpan AddTourLogTotalTime
        {
            get => _addTourLogTotalTime;
            set
            {
                _addTourLogTotalTime = value;
                OnPropertyChanged(nameof(AddTourLogTotalTime));
            }
        }

        private float _addTourLogRating;
        public float AddTourLogRating
        {
            get => _addTourLogRating;
            set
            {
                _addTourLogRating = value;
                OnPropertyChanged(nameof(AddTourLogRating));
            }
        }

        public ICommand CreateAddTourLogCommand { get; }
        public ICommand CancelAddTourLogCommand { get; }

        public AddTourLogViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            CreateAddTourLogCommand = new CreateAddTourLogCommand(navigationService, tourPlannerManager, this, sharedDataService);
            CancelAddTourLogCommand = new CancelAddTourLogCommand(navigationService);
        }
    }
}
