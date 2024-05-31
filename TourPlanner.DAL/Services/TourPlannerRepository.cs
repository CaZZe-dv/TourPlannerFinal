using System.Windows.Media.Imaging;
using TourPlanner.BL.Models;
using TourPlanner.BL.Services;

namespace TourPlanner.DAL.Services
{
    public class TourPlannerRepository
    {
        private readonly ITour _tourHandler;
        private readonly ITourLog _tourLogHandler;
        private readonly IRouteService _routeService;
        private readonly IMapService _mapService;
        private readonly IImageService _imageService;

        public TourPlannerRepository(ITour tourHandler, ITourLog tourLogHandler, IRouteService routeService, IMapService mapService, IImageService imageService)
        {
            _tourHandler = tourHandler;
            _tourLogHandler = tourLogHandler;
            _routeService = routeService;
            _mapService = mapService;
            _imageService = imageService;
        }
        public async Task<BitmapSource> GetImage(string tourId)
        {
            return await _imageService.GetImage(tourId);
        }

        public async Task<BitmapSource?> GetRouteImage(string x, string y)
        {
            return await _mapService.GenerateMapAsync(x, y);
        }

        public async Task<RouteResponse?> GetRouteInformation(string transportType, string start, string end)
        {
            return await _routeService.GetRouteResponse(transportType,start,end);
        }

        public async Task<IEnumerable<Tour>> GetAllTours()
        {
            return await _tourHandler.GetAllTours();
        }

        public async Task AddTour(Tour tour)
        {
            Guid id = await _tourHandler.CreateTour(tour);
            await _imageService.SaveImage(tour.RouteInformation, id.ToString());
        }

        public async Task UpdateTour(Tour tour)
        {
            await _tourHandler.UpdateTour(tour);
        }

        public async Task DeleteTour(Tour tour)
        {
            await _tourHandler.DeleteTour(tour);
        }

        public async Task UpdateTourLog(TourLog tourLog)
        {
            await _tourLogHandler.UpdateTourLog(tourLog);
        }

        public async Task DeleteTourLog(TourLog tourLog)
        {
            await _tourLogHandler.DeleteTourLog(tourLog);
        }

        public async Task CreateTourLog(TourLog tourLog)
        {
            await _tourLogHandler.CreateTourLog(tourLog);
        }

        public async Task RemoveAllTourLogs(Tour tour)
        {
            await _tourLogHandler.RemoveAllTourLogs(tour);
        }

        public async Task<IEnumerable<TourLog>> GetAllTourLogs(Tour tour)
        {
            return await _tourLogHandler.GetAllTourLogs(tour);
        }
    }
}
