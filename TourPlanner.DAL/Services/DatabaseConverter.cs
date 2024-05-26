using TourPlanner.BL.Models;
using TourPlanner.DAL.Dtos;

namespace TourPlanner.DAL.Services
{
    public abstract class DatabaseConverter
    {
        protected Tour ToTour(TourDTO tourDTO)
        {
            return new Tour(tourDTO.Id, tourDTO.Name, tourDTO.Description, tourDTO.From, tourDTO.To,
                tourDTO.TransportType, tourDTO.TourDistance, tourDTO.EstimatedTime, null);
        }

        protected TourLog ToTourLog(TourLogDTO tourLogDTO)
        {
            return new TourLog(tourLogDTO.Id, DateTime.Parse(tourLogDTO.DateTime), tourLogDTO.Comment,
                tourLogDTO.Difficulty, tourLogDTO.TotalDistance, tourLogDTO.TotalTime,
                tourLogDTO.Rating, tourLogDTO.TourId);
        }

        protected TourDTO ToTourDTO(Tour tour)
        {
            return new TourDTO()
            {
                Name = tour.Name,
                Description = tour.Description,
                From = tour.From,
                To = tour.To,
                TransportType = tour.TransportType,
                TourDistance = tour.TourDistance,
                EstimatedTime = tour.EstimatedTime,
            };
        }

        protected TourLogDTO ToTourLogDTO(TourLog tourLog)
        {
            return new TourLogDTO()
            {
                DateTime = tourLog.DateTime.ToString(),
                Comment = tourLog.Comment,
                Difficulty = tourLog.Difficulty,
                TotalDistance = tourLog.TotalDistance,
                TotalTime = tourLog.TotalTime,
                Rating = tourLog.Rating,
                TourId = tourLog.TourId
            };
        }
    }
}
