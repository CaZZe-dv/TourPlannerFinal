using TourPlanner.BL.Models;

namespace TourPlanner.DAL.Services
{
    public interface ITour
    {
        Task CreateTour(Tour tour);
        Task DeleteTour(Tour tour);
        Task<IEnumerable<Tour>> GetAllTours();
        Task UpdateTour(Tour tour);
    }
}
