using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class EditTourViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private string _editTourName;
        public string EditTourName
        {
            get => _editTourName;
            set
            {
                logger.Info($"EditTourName changed from '{_editTourName}' to '{value}'");
                _editTourName = value;
                OnPropertyChanged(nameof(EditTourName));
            }
        }

        private string _editTourDescription;
        public string EditTourDescription
        {
            get => _editTourDescription;
            set
            {
                logger.Info($"EditTourDescription changed from '{_editTourDescription}' to '{value}'");
                _editTourDescription = value;
                OnPropertyChanged(nameof(EditTourDescription));
            }
        }

        private string _editTourFrom;
        public string EditTourFrom
        {
            get => _editTourFrom;
            set
            {
                logger.Info($"EditTourFrom changed from '{_editTourFrom}' to '{value}'");
                _editTourFrom = value;
                OnPropertyChanged(nameof(EditTourFrom));
            }
        }

        private string _editTourTo;
        public string EditTourTo
        {
            get => _editTourTo;
            set
            {
                logger.Info($"EditTourTo changed from '{_editTourTo}' to '{value}'");
                _editTourTo = value;
                OnPropertyChanged(nameof(EditTourTo));
            }
        }

        public List<string> TransportTypes { get; } = new List<string> { "Walking", "Driving", "Cycling" };

        private string _editTourTransportType;
        public string EditTourTransportType
        {
            get => _editTourTransportType;
            set
            {
                logger.Info($"EditTourTransportType changed from '{_editTourTransportType}' to '{value}'");
                _editTourTransportType = value;
                OnPropertyChanged(nameof(EditTourTransportType));
            }
        }

        private string _editTourDistance;
        public string EditTourDistance
        {
            get => _editTourDistance;
            set
            {
                logger.Info($"EditTourDistance changed from '{_editTourDistance}' to '{value}'");
                _editTourDistance = value;
                OnPropertyChanged(nameof(EditTourDistance));
            }
        }

        private string _editTourEstimatedTime;
        public string EditTourEstimatedTime
        {
            get => _editTourEstimatedTime;
            set
            {
                logger.Info($"EditTourEstimatedTime changed from '{_editTourEstimatedTime}' to '{value}'");
                _editTourEstimatedTime = value;
                OnPropertyChanged(nameof(EditTourEstimatedTime));
            }
        }

        private BitmapSource _editTourImage;
        public BitmapSource EditTourImage
        {
            get => _editTourImage;
            set
            {
                logger.Info($"EditTourImage changed");
                _editTourImage = value;
                OnPropertyChanged(nameof(EditTourImage));
            }
        }

        private bool _isTourChanged;
        public bool IsTourChanged
        {
            get => _isTourChanged;
            set
            {
                logger.Info($"IsTourChanged changed from '{_isTourChanged}' to '{value}'");
                _isTourChanged = value;
                OnPropertyChanged(nameof(IsTourChanged));
            }
        }

        private bool _isRouteInformationFetched;
        public bool IsRouteInformationFetched
        {
            get => _isRouteInformationFetched;
            set
            {
                logger.Info($"IsRouteInformationFetched changed from '{_isRouteInformationFetched}' to '{value}'");
                _isRouteInformationFetched = value;
                OnPropertyChanged(nameof(IsRouteInformationFetched));
            }
        }

        public ICommand UpdateEditTourCommand { get; }
        public ICommand CancelEditTourCommand { get; }
        public ICommand LoadRouteInformation { get; }
        public ICommand LoadImageFromFile { get; }

        public EditTourViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            logger.Info("Initializing EditTourViewModel.");

            IsRouteInformationFetched = true;

            Tour existingTour = sharedDataService.SelectedTour;

            _editTourName = existingTour.Name;
            _editTourFrom = existingTour.From.ToString();
            _editTourTo = existingTour.To.ToString();
            _editTourDescription = existingTour.Description;
            _editTourTransportType = existingTour.TransportType;
            _editTourDistance = existingTour.TourDistance.ToString();
            _editTourEstimatedTime = existingTour.EstimatedTime.ToString();

            logger.Info("EditTourViewModel initialized with existing tour data.");

            UpdateEditTourCommand = new UpdateEditTourCommand(this, tourPlannerManager, navigationService, sharedDataService);
            CancelEditTourCommand = new CancelEditTourCommand(navigationService);
            LoadRouteInformation = new EditLoadRouteInformationCommand(this, tourPlannerManager);
            LoadImageFromFile = new LoadImageFromFileCommand(this, tourPlannerManager, sharedDataService);

            LoadImageFromFile.Execute(null);

            IsTourChanged = false;
            logger.Info("Executed LoadImageFromFile command.");
        }
    }
}
