using System.ComponentModel;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class UpdateEditTourLogCommand : AsyncCommandBase
    {
        private readonly TourPlannerManager _tourPlannerManager;
        private readonly NavigationService _navigationService;
        private readonly SharedDataService _sharedDataService;
        private readonly EditTourLogViewModel _editTourLogViewModel;

        public UpdateEditTourLogCommand(TourPlannerManager tourPlannerManager, NavigationService navigationService, EditTourLogViewModel editTourLogViewModel, SharedDataService sharedDataService)
        {
            _tourPlannerManager = tourPlannerManager;
            _navigationService = navigationService;
            _editTourLogViewModel = editTourLogViewModel;
            _sharedDataService = sharedDataService;
            _editTourLogViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && _editTourLogViewModel.EditTourLogDateTime != null &&
                !string.IsNullOrEmpty(_editTourLogViewModel.EditTourLogComment) && _editTourLogViewModel.EditTourLogDifficulty > 0 &&
                _editTourLogViewModel.EditTourLogTotalDistance > 0 && _editTourLogViewModel.EditTourLogTotalTime != null &&
                _editTourLogViewModel.EditTourLogRating > 0;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_editTourLogViewModel.EditTourLogDateTime) ||
                e.PropertyName == nameof(_editTourLogViewModel.EditTourLogComment) ||
                e.PropertyName == nameof(_editTourLogViewModel.EditTourLogDifficulty) ||
                e.PropertyName == nameof(_editTourLogViewModel.EditTourLogTotalDistance) ||
                e.PropertyName == nameof(_editTourLogViewModel.EditTourLogTotalTime) ||
                e.PropertyName == nameof(_editTourLogViewModel.EditTourLogRating))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            TourLog updatedTourLog = new TourLog(
                _sharedDataService.SelectedTourLog.Id,
                _editTourLogViewModel.EditTourLogDateTime,
                _editTourLogViewModel.EditTourLogComment,
                _editTourLogViewModel.EditTourLogDifficulty,
                _editTourLogViewModel.EditTourLogTotalDistance,
                _editTourLogViewModel.EditTourLogTotalTime,
                _editTourLogViewModel.EditTourLogRating,
                _sharedDataService.SelectedTour.Id);

            await _tourPlannerManager.UpdateTourLog(updatedTourLog);

            _navigationService.Navigate();
        }
    }
}
