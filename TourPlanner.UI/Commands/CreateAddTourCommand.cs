using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class CreateAddTourCommand : AsyncCommandBase
    {
        private readonly NavigationService _navigationService;
        private readonly AddTourViewModel _addTourViewModel;
        private readonly TourPlannerRepository _tourPlannerManager;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public CreateAddTourCommand(AddTourViewModel addTourViewModel, TourPlannerRepository tourPlannerManager, NavigationService navigationService)
        {
            _addTourViewModel = addTourViewModel;
            _navigationService = navigationService;
            _tourPlannerManager = tourPlannerManager;
            _addTourViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addTourViewModel.AddTourName) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourFrom) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourTo) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourDescription) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourTransportType) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourDistance) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourEstimatedTime)
                || e.PropertyName == nameof(_addTourViewModel.AddTourImage)
                || e.PropertyName == nameof(_addTourViewModel.IsRouteInformationFetched))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            bool canExecute = base.CanExecute(parameter) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourName) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourFrom) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourTo) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourDescription) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourTransportType) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourDistance) &&
                !string.IsNullOrEmpty(_addTourViewModel.AddTourEstimatedTime) &&
                _addTourViewModel.IsRouteInformationFetched &&
                _addTourViewModel.AddTourImage != null;

            logger.Debug($"CreateAddTourCommand CanExecute: {canExecute}");

            return canExecute;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            logger.Info("Creating a new tour...");
            _addTourViewModel.IsLoadingAddTour = true;
            try
            {
                await _tourPlannerManager.AddTour(new Tour(
                    _addTourViewModel.AddTourName,
                    _addTourViewModel.AddTourDescription,
                    _addTourViewModel.AddTourFrom,
                    _addTourViewModel.AddTourTo,
                    _addTourViewModel.AddTourTransportType,
                    float.Parse(_addTourViewModel.AddTourDistance),
                    TimeSpan.Parse(_addTourViewModel.AddTourEstimatedTime),
                    _addTourViewModel.AddTourImage));

                logger.Info("New tour created successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while creating a new tour: {ex.Message}");
                // Handle the exception
            }
            finally
            {
                _addTourViewModel.IsLoadingAddTour = false;
            }
            _navigationService.Navigate();
        }
    }
}
