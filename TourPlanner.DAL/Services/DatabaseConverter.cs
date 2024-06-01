using System;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Dtos;
using TourPlanner.Utility.Logging;

namespace TourPlanner.DAL.Services
{
    public abstract class DatabaseConverter
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        protected Tour ToTour(TourDTO tourDTO)
        {
            logger.Debug("Converting TourDTO to Tour.");
            Tour tour = new Tour(tourDTO.Id, tourDTO.Name, tourDTO.Description, tourDTO.From, tourDTO.To,
                tourDTO.TransportType, tourDTO.TourDistance, tourDTO.EstimatedTime, null);
            logger.Debug("TourDTO converted to Tour successfully.");
            return tour;
        }

        protected TourLog ToTourLog(TourLogDTO tourLogDTO)
        {
            logger.Debug("Converting TourLogDTO to TourLog.");
            TourLog tourLog = new TourLog(tourLogDTO.Id, DateTime.Parse(tourLogDTO.DateTime), tourLogDTO.Comment,
                tourLogDTO.Difficulty, tourLogDTO.TotalDistance, tourLogDTO.TotalTime,
                tourLogDTO.Rating, tourLogDTO.TourId);
            logger.Debug("TourLogDTO converted to TourLog successfully.");
            return tourLog;
        }

        protected TourDTO ToTourDTO(Tour tour)
        {
            logger.Debug("Converting Tour to TourDTO.");
            TourDTO tourDTO = new TourDTO()
            {
                Name = tour.Name,
                Description = tour.Description,
                From = tour.From,
                To = tour.To,
                TransportType = tour.TransportType,
                TourDistance = tour.TourDistance,
                EstimatedTime = tour.EstimatedTime,
            };
            logger.Debug("Tour converted to TourDTO successfully.");
            return tourDTO;
        }

        protected TourLogDTO ToTourLogDTO(TourLog tourLog)
        {
            logger.Debug("Converting TourLog to TourLogDTO.");
            TourLogDTO tourLogDTO = new TourLogDTO()
            {
                DateTime = tourLog.DateTime.ToString(),
                Comment = tourLog.Comment,
                Difficulty = tourLog.Difficulty,
                TotalDistance = tourLog.TotalDistance,
                TotalTime = tourLog.TotalTime,
                Rating = tourLog.Rating,
                TourId = tourLog.TourId
            };
            logger.Debug("TourLog converted to TourLogDTO successfully.");
            return tourLogDTO;
        }
    }
}
