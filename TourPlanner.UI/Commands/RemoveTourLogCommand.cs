using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class RemoveTourLogCommand : AsyncCommandBase
    {
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly TourPlannerRepository _tourPlannerManager;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

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
            bool canExecute = _mainMenuViewModel.SelectedTourLog != null && base.CanExecute(parameter);
            logger.Debug($"RemoveTourLogCommand CanExecute: {canExecute}");
            return canExecute;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            logger.Info("Removing tour log...");
            try
            {
                await _tourPlannerManager.DeleteTourLog(_mainMenuViewModel.SelectedTourLog.TourLog);
                _mainMenuViewModel.LoadTourLogCommand.Execute(null);
                logger.Info("Tour log removed successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while removing tour log: {ex.Message}");
                // Handle the exception
            }
        }
    }
}
