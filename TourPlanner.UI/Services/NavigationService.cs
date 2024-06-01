using System;
using TourPlanner.Utility.Logging;
using TourPlanner.UI.ViewModels;
using TourPlanner.UI.Stores;

namespace TourPlanner.UI.Services
{
    public class NavigationService
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            ViewModelBase viewModel = _createViewModel();
            logger.Info($"Navigating to {viewModel.GetType().Name}.");
            _navigationStore.CurrentViewModel = viewModel;
        }
    }
}
