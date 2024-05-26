using TourPlanner.BL.Models;

namespace TourPlanner.UI.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        public readonly Tour Tour;
        public string Name => Tour.Name;

        public TourViewModel(Tour tour)
        {
            Tour = tour;
        }
    }
}