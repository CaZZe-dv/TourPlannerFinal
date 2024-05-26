using System.ComponentModel;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class CreateAddTourLogCommand : AsyncCommandBase
    {
        private readonly NavigationService _navigationService;
        private readonly TourPlannerManager _tourPlannerManager;
        private readonly AddTourLogViewModel _addTourLogViewModel;
        private readonly SharedDataService _sharedDataService;

        public CreateAddTourLogCommand(NavigationService navigationService, TourPlannerManager tourPlannerManager, AddTourLogViewModel addTourLogViewModel, SharedDataService sharedDataService)
        {
            _navigationService = navigationService;
            _tourPlannerManager = tourPlannerManager;
            _addTourLogViewModel = addTourLogViewModel;
            _sharedDataService = sharedDataService;
            _addTourLogViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addTourLogViewModel.AddTourLogDateTime) ||
                e.PropertyName == nameof(_addTourLogViewModel.AddTourLogComment) ||
                e.PropertyName == nameof(_addTourLogViewModel.AddTourLogDifficulty) ||
                e.PropertyName == nameof(_addTourLogViewModel.AddTourLogTotalDistance) ||
                e.PropertyName == nameof(_addTourLogViewModel.AddTourLogTotalTime) ||
                e.PropertyName == nameof(_addTourLogViewModel.AddTourLogRating))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && _addTourLogViewModel.AddTourLogDateTime != null &&
                !string.IsNullOrEmpty(_addTourLogViewModel.AddTourLogComment) && _addTourLogViewModel.AddTourLogDifficulty > 0 &&
                _addTourLogViewModel.AddTourLogTotalDistance > 0 && _addTourLogViewModel.AddTourLogTotalTime != null &&
                _addTourLogViewModel.AddTourLogRating > 0;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            TourLog newTourLog = new TourLog(_addTourLogViewModel.AddTourLogDateTime,
                _addTourLogViewModel.AddTourLogComment,
                _addTourLogViewModel.AddTourLogDifficulty,
                _addTourLogViewModel.AddTourLogTotalDistance,
                _addTourLogViewModel.AddTourLogTotalTime,
                _addTourLogViewModel.AddTourLogRating,
                _sharedDataService.SelectedTour.Id);

            await _tourPlannerManager.CreateTourLog(newTourLog);
            _navigationService.Navigate();
        }
    }
}
