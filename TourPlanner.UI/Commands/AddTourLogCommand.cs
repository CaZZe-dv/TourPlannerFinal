using System.ComponentModel;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class AddTourLogCommand : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationService _navigationService;
        private readonly MainMenuViewModel _mainMenuViewModel;

        public AddTourLogCommand(MainMenuViewModel mainMenuViewModel, NavigationService navigationService)
        {
            _navigationService = navigationService;
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
            logger.Debug($"CanExecute: {canExecute}");
            return canExecute;
        }

        public override void Execute(object? parameter)
        {
            logger.Info("Executing AddTourLogCommand.");
            _navigationService.Navigate();
        }
    }
}
