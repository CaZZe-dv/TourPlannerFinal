using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class RemoveTourCommand : AsyncCommandBase
    {
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly MainMenuViewModel _mainMenuViewModel;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public RemoveTourCommand(MainMenuViewModel mainMenuViewModel, TourPlannerRepository tourPlannerManager)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _tourPlannerManager = tourPlannerManager;
            _mainMenuViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool canExecute = _mainMenuViewModel.SelectedTour != null && base.CanExecute(parameter);
            logger.Debug($"RemoveTourCommand CanExecute: {canExecute}");
            return canExecute;
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
            logger.Info("Removing tour and its logs...");
            try
            {
                await _tourPlannerManager.DeleteTour(_mainMenuViewModel.SelectedTour.Tour);
                await _tourPlannerManager.RemoveAllTourLogs(_mainMenuViewModel.SelectedTour.Tour);
                _mainMenuViewModel.LoadTourCommand.Execute(null);
                logger.Info("Tour and its logs removed successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while removing tour and its logs: {ex.Message}");
                // Handle the exception
            }
        }
    }
}
