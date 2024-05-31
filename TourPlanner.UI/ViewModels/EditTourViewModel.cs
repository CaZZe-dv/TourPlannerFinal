using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;

namespace TourPlanner.UI.ViewModels
{
    public class EditTourViewModel : ViewModelBase
    {
        private string _editTourName;
        public string EditTourName
        {
            get => _editTourName;
            set
            {
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
            IsTourChanged = false;
            IsRouteInformationFetched = true;

            Tour existingTour = sharedDataService.SelectedTour;

            _editTourName = existingTour.Name;
            _editTourFrom = existingTour.From.ToString();
            _editTourTo = existingTour.To.ToString();
            _editTourDescription = existingTour.Description;
            _editTourTransportType = existingTour.TransportType;
            _editTourDistance = existingTour.TourDistance.ToString();
            _editTourEstimatedTime = existingTour.EstimatedTime.ToString();

            UpdateEditTourCommand = new UpdateEditTourCommand(this, tourPlannerManager, navigationService, sharedDataService);
            CancelEditTourCommand = new CancelEditTourCommand(navigationService);
            LoadRouteInformation = new EditLoadRouteInformationCommand(this, tourPlannerManager);
            LoadImageFromFile = new LoadImageFromFileCommand(this, tourPlannerManager, sharedDataService);
            LoadImageFromFile.Execute(null);
        }
    }
}
