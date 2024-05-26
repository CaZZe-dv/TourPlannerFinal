using TourPlanner.BL.Models;

namespace TourPlanner.DAL.Services
{
    public class SharedDataService
    {
        public Tour? SelectedTour { get; set; }
        public TourLog? SelectedTourLog { get; set; }
    }
}
