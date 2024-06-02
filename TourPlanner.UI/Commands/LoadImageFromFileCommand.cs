using System;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public class LoadImageFromFileCommand : AsyncCommandBase
    {
        private readonly EditTourViewModel _editTourViewModel;
        private readonly TourPlannerRepository _tourPlannerRepository;
        private readonly SharedDataService _sharedDataService;

        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public LoadImageFromFileCommand(EditTourViewModel editTourViewModel, TourPlannerRepository tourPlannerRepository, SharedDataService sharedDataService)
        {
            _editTourViewModel = editTourViewModel;
            _tourPlannerRepository = tourPlannerRepository;
            _sharedDataService = sharedDataService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _editTourViewModel.IsLoadingEditTour = true;
            try
            {
                logger.Info("Loading image from file...");
                _editTourViewModel.EditTourImage = await _tourPlannerRepository.GetImage(_sharedDataService.SelectedTour.Id.ToString());
                logger.Info("Image loaded from file successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Error loading image from file: {ex.Message}");
                // Handle the exception
            }
            finally
            {
                _editTourViewModel.IsLoadingEditTour = false;
            }
        }
    }
}
