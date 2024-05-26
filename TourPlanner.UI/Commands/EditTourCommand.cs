using System.ComponentModel;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class EditTourCommand : CommandBase
    {
        private readonly NavigationService _editTourViewNavigationService;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public EditTourCommand(MainMenuViewModel mainMenuViewModel, NavigationService editTourViewNavigationService)
        {
            _editTourViewNavigationService = editTourViewNavigationService;
            _mainMenuViewModel = mainMenuViewModel;
            _mainMenuViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mainMenuViewModel.SelectedTour))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _mainMenuViewModel.SelectedTour != null && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _editTourViewNavigationService.Navigate();
        }
    }
}
