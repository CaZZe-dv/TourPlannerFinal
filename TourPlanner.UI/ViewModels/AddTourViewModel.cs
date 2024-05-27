using System.Windows.Input;
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

        public ICommand CreateAddTourCommand { get; }
        public ICommand CancelAddTourCommand { get; }

        public AddTourViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService)
        {
            CreateAddTourCommand = new CreateAddTourCommand(this, tourPlannerManager, navigationService);
            CancelAddTourCommand = new CancelAddTourCommand(navigationService);
        }
    }
}
