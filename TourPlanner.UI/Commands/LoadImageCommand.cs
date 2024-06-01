using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class LoadImageCommand : AsyncCommandBase
    {
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly TourPlannerRepository _tourPlannerRepository;
        private readonly SharedDataService _sharedDataService;
        public LoadImageCommand(MainMenuViewModel mainMenuViewModel, TourPlannerRepository tourPlannerRepository, SharedDataService sharedDataService)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _tourPlannerRepository = tourPlannerRepository;
            _sharedDataService = sharedDataService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _mainMenuViewModel.MainMenuImage = await _tourPlannerRepository.GetImage(_sharedDataService.SelectedTour.Id.ToString());
        }
    }
}
