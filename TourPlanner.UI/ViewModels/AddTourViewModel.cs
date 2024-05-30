using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;

namespace TourPlanner.UI.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        private string _addTourName;
        public string AddTourName
        {
            get => _addTourName;
            set
            {
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
                _isRouteInformationFetched = value;
                OnPropertyChanged(nameof(IsRouteInformationFetched));
            }
        }

        public ICommand CreateAddTourCommand { get; }
        public ICommand CancelAddTourCommand { get; }

        public ICommand LoadRouteInformation { get; }

        public AddTourViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService)
        {
            CreateAddTourCommand = new CreateAddTourCommand(this, tourPlannerManager, navigationService);
            CancelAddTourCommand = new CancelAddTourCommand(navigationService);
            LoadRouteInformation = new LoadRouteInformationCommand(this, tourPlannerManager);
        }
    }
}
