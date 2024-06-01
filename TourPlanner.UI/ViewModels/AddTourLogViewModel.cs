using System;
using System.Windows.Input;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class AddTourLogViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private DateTime _addTourLogDateTime;
        public DateTime AddTourLogDateTime
        {
            get => _addTourLogDateTime;
            set
            {
                logger.Info($"AddTourLogDateTime changed from {_addTourLogDateTime} to {value}");
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
                logger.Info($"AddTourLogComment changed from '{_addTourLogComment}' to '{value}'");
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
                logger.Info($"AddTourLogDifficulty changed from {_addTourLogDifficulty} to {value}");
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
                logger.Info($"AddTourLogTotalDistance changed from {_addTourLogTotalDistance} to {value}");
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
                logger.Info($"AddTourLogTotalTime changed from {_addTourLogTotalTime} to {value}");
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
                logger.Info($"AddTourLogRating changed from {_addTourLogRating} to {value}");
                _addTourLogRating = value;
                OnPropertyChanged(nameof(AddTourLogRating));
            }
        }

        public ICommand CreateAddTourLogCommand { get; }
        public ICommand CancelAddTourLogCommand { get; }

        public AddTourLogViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            logger.Info("Initializing AddTourLogViewModel.");

            CreateAddTourLogCommand = new CreateAddTourLogCommand(navigationService, tourPlannerManager, this, sharedDataService);
            CancelAddTourLogCommand = new CancelAddTourLogCommand(navigationService);

            logger.Info("AddTourLogViewModel initialized successfully.");
        }
    }
}
