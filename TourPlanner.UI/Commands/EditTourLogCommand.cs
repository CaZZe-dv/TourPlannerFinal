using System;
using System.ComponentModel;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class EditTourLogCommand : CommandBase
    {
        private readonly NavigationService _navigationService;
        private readonly MainMenuViewModel _mainMenuViewModel;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public EditTourLogCommand(MainMenuViewModel mainMenuViewModel, NavigationService navigationService)
        {
            _navigationService = navigationService;
            _mainMenuViewModel = mainMenuViewModel;
            _mainMenuViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mainMenuViewModel.SelectedTourLog))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            bool canExecute = _mainMenuViewModel.SelectedTourLog != null && base.CanExecute(parameter);
            logger.Debug($"EditTourLogCommand CanExecute: {canExecute}");
            return canExecute;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Navigating to edit tour log view...");
            _navigationService.Navigate();
            logger.Info("Navigation to edit tour log view completed.");
        }
    }
}
