using System;
using System.ComponentModel;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class EditTourCommand : CommandBase
    {
        private readonly NavigationService _editTourViewNavigationService;
        private readonly MainMenuViewModel _mainMenuViewModel;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

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
            bool canExecute = _mainMenuViewModel.SelectedTour != null && base.CanExecute(parameter);
            logger.Debug($"EditTourCommand CanExecute: {canExecute}");
            return canExecute;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Navigating to edit tour view...");
            _editTourViewNavigationService.Navigate();
            logger.Info("Navigation to edit tour view completed.");
        }
    }
}
