using TourPlanner.BL.Models;

namespace TourPlanner.DAL.Services
{
    public interface ITourLog
    {
        Task CreateTourLog(TourLog tourLog);
        Task UpdateTourLog(TourLog tourLog);
        Task RemoveAllTourLogs(Tour tour);
        Task<IEnumerable<TourLog>> GetAllTourLogs(Tour tour);
        Task DeleteTourLog(TourLog tourLog);
    }
}
