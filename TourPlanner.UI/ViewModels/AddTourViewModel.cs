using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private string _addTourName;
        public string AddTourName
        {
            get => _addTourName;
            set
            {
                logger.Info($"AddTourName changed from '{_addTourName}' to '{value}'");
                _addTourName = value;
                OnPropertyChanged(nameof(AddTourName));
            }
        }

        private string _addTourDescription;
        public string AddTourDescription
        {
            get => _addTourDescription;
            set
            {
                logger.Info($"AddTourDescription changed from '{_addTourDescription}' to '{value}'");
                _addTourDescription = value;
                OnPropertyChanged(nameof(AddTourDescription));
            }
        }

        private string _addTourFrom;
        public string AddTourFrom
        {
            get => _addTourFrom;
            set
            {
                logger.Info($"AddTourFrom changed from '{_addTourFrom}' to '{value}'");
                _addTourFrom = value;
                OnPropertyChanged(nameof(AddTourFrom));
            }
        }

        private string _addTourTo;
        public string AddTourTo
        {
            get => _addTourTo;
            set
            {
                logger.Info($"AddTourTo changed from '{_addTourTo}' to '{value}'");
                _addTourTo = value;
                OnPropertyChanged(nameof(AddTourTo));
            }
        }

        public List<string> TransportTypes { get; } = new List<string> { "Walking", "Driving", "Cycling" };

        private string _addTourTransportType;
        public string AddTourTransportType
        {
            get => _addTourTransportType;
            set
            {
                logger.Info($"AddTourTransportType changed from '{_addTourTransportType}' to '{value}'");
                _addTourTransportType = value;
                OnPropertyChanged(nameof(AddTourTransportType));
            }
        }

        private string _addTourDistance;
        public string AddTourDistance
        {
            get => _addTourDistance;
            set
            {
                logger.Info($"AddTourDistance changed from '{_addTourDistance}' to '{value}'");
                _addTourDistance = value;
                OnPropertyChanged(nameof(AddTourDistance));
            }
        }

        private string _addTourEstimatedTime;
        public string AddTourEstimatedTime
        {
            get => _addTourEstimatedTime;
            set
            {
                logger.Info($"AddTourEstimatedTime changed from '{_addTourEstimatedTime}' to '{value}'");
                _addTourEstimatedTime = value;
                OnPropertyChanged(nameof(AddTourEstimatedTime));
            }
        }

        private BitmapSource _addTourImage;
        public BitmapSource AddTourImage
        {
            get => _addTourImage;
            set
            {
                logger.Info("AddTourImage changed");
                _addTourImage = value;
                OnPropertyChanged(nameof(AddTourImage));
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

        private bool _isLoadingAddTour;
        public bool IsLoadingAddTour
        {
            get => _isLoadingAddTour;
            set
            {
                logger.Info($"IsLoadingAddTour changed from '{_isLoadingAddTour}' to '{value}'");
                _isLoadingAddTour = value;
                OnPropertyChanged(nameof(IsLoadingAddTour));
            }
        }

        public ICommand CreateAddTourCommand { get; }
        public ICommand CancelAddTourCommand { get; }
        public ICommand LoadRouteInformation { get; }

        public AddTourViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService)
        {
            logger.Info("Initializing AddTourViewModel.");

            CreateAddTourCommand = new CreateAddTourCommand(this, tourPlannerManager, navigationService);
            CancelAddTourCommand = new CancelAddTourCommand(navigationService);
            LoadRouteInformation = new LoadRouteInformationCommand(this, tourPlannerManager);

            logger.Info("AddTourViewModel initialized successfully.");
        }
    }
}
