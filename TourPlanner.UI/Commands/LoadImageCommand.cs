using System;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class LoadImageCommand : AsyncCommandBase
    {
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly TourPlannerRepository _tourPlannerRepository;
        private readonly SharedDataService _sharedDataService;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public LoadImageCommand(MainMenuViewModel mainMenuViewModel, TourPlannerRepository tourPlannerRepository, SharedDataService sharedDataService)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _tourPlannerRepository = tourPlannerRepository;
            _sharedDataService = sharedDataService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                logger.Info("Loading image...");
                _mainMenuViewModel.MainMenuImage = await _tourPlannerRepository.GetImage(_sharedDataService.SelectedTour.Id.ToString());
                logger.Info("Image loaded successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error loading image: {ex.Message}");
                // Handle the exception
            }
        }
    }
}
