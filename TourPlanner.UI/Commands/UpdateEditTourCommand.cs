using System.ComponentModel;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class UpdateEditTourCommand : AsyncCommandBase
    {
        private readonly NavigationService _navigationService;
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly EditTourViewModel _editTourViewModel;
        private readonly SharedDataService _sharedDataService;

        public UpdateEditTourCommand(EditTourViewModel editTourViewModel, TourPlannerRepository tourPlannerManager, NavigationService navigationService, SharedDataService sharedDataService)
        {
            _editTourViewModel = editTourViewModel;
            _tourPlannerManager = tourPlannerManager;
            _navigationService = navigationService;
            _sharedDataService = sharedDataService;
            _editTourViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && !string.IsNullOrEmpty(_editTourViewModel.EditTourName)
                && !string.IsNullOrEmpty(_editTourViewModel.EditTourFrom)
                && !string.IsNullOrEmpty(_editTourViewModel.EditTourTo)
                && !string.IsNullOrEmpty(_editTourViewModel.EditTourDescription)
                && !string.IsNullOrEmpty(_editTourViewModel.EditTourTransportType);
        }
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_editTourViewModel.EditTourName) ||
                e.PropertyName == nameof(_editTourViewModel.EditTourFrom) ||
                e.PropertyName == nameof(_editTourViewModel.EditTourTo) ||
                e.PropertyName == nameof(_editTourViewModel.EditTourDescription) ||
                e.PropertyName == nameof(_editTourViewModel.EditTourTransportType))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Tour updatedTour = new Tour(_sharedDataService.SelectedTour.Id,
                _editTourViewModel.EditTourName,
                _editTourViewModel.EditTourDescription,
                _editTourViewModel.EditTourFrom,
                _editTourViewModel.EditTourTo,
                _editTourViewModel.EditTourTransportType,
                0,
                TimeSpan.Zero,
                null);
            await _tourPlannerManager.UpdateTour(updatedTour);
            _navigationService.Navigate();
        }
    }
}
