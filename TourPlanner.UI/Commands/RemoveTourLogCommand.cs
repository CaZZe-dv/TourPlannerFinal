using System.ComponentModel;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class RemoveTourLogCommand : AsyncCommandBase
    {
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly TourPlannerRepository _tourPlannerManager;

        public RemoveTourLogCommand(MainMenuViewModel mainMenuViewModel, TourPlannerRepository tourPlannerManager)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mainMenuViewModel.SelectedTourLog))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _mainMenuViewModel.SelectedTourLog != null && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _tourPlannerManager.DeleteTourLog(_mainMenuViewModel.SelectedTourLog.TourLog);
            _mainMenuViewModel.LoadTourLogCommand.Execute(null);
        }
    }
}
