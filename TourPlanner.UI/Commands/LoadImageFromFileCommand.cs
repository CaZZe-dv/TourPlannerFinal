using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class LoadImageFromFileCommand : AsyncCommandBase
    {

        private readonly EditTourViewModel _editTourViewModel;
        private readonly TourPlannerRepository _tourPlannerRepository;
        private readonly SharedDataService _sharedDataService;
        public LoadImageFromFileCommand(EditTourViewModel editTourViewModel, TourPlannerRepository tourPlannerRepository, SharedDataService sharedDataService)
        {
            _editTourViewModel = editTourViewModel;
            _tourPlannerRepository = tourPlannerRepository;
            _sharedDataService = sharedDataService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _editTourViewModel.EditTourImage = await _tourPlannerRepository.GetImage(_sharedDataService.SelectedTour.Id.ToString());
        }
    }
}
