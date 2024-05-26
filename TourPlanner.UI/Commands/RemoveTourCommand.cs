using System.ComponentModel;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class RemoveTourCommand : AsyncCommandBase
    {
        private readonly TourPlannerManager _tourPlannerManager;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public RemoveTourCommand(MainMenuViewModel mainMenuViewModel, TourPlannerManager tourPlannerManager)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _mainMenuViewModel.SelectedTour != null && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mainMenuViewModel.SelectedTour))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _tourPlannerManager.DeleteTour(_mainMenuViewModel.SelectedTour.Tour);
            await _tourPlannerManager.RemoveAllTourLogs(_mainMenuViewModel.SelectedTour.Tour);
            _mainMenuViewModel.LoadTourCommand.Execute(null);
        }

    }
}
