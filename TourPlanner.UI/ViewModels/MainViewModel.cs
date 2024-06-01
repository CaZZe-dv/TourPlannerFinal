using TourPlanner.UI.Stores;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            logger.Info("Initializing MainViewModel.");

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            logger.Info("MainViewModel initialized.");
        }

        private void OnCurrentViewModelChanged()
        {
            logger.Info("CurrentViewModel changed.");
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
