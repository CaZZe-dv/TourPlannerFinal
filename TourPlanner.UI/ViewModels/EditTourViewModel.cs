using System.Windows.Input;
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

        public ICommand UpdateEditTourCommand { get; }
        public ICommand CancelEditTourCommand { get; }

        public EditTourViewModel(TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            Tour existingTour = sharedDataService.SelectedTour;

            _editTourName = existingTour.Name;
            _editTourFrom = existingTour.From;
            _editTourTo = existingTour.To;
            _editTourDescription = existingTour.Description;
            _editTourTransportType = existingTour.TransportType;

            UpdateEditTourCommand = new UpdateEditTourCommand(this, tourPlannerManager, navigationService, sharedDataService);
            CancelEditTourCommand = new CancelEditTourCommand(navigationService);
        }
    }
}
