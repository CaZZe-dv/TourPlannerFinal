using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourPlanner.BL.Models;
using TourPlanner.DAL.DbContexts;
using TourPlanner.DAL.Dtos;
using TourPlanner.Utility.Logging;

namespace TourPlanner.DAL.Services
{
    public class DatabaseTour : DatabaseConverter, ITour
    {
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public DatabaseTour(TourPlannerDbContextFactory tourPlannerDbContextFactory)
        {
            _tourPlannerDbContextFactory = tourPlannerDbContextFactory;
        }

        public async Task<Guid> CreateTour(Tour tour)
        {
            logger.Info($"Creating new tour: {tour.Name}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    TourDTO tourDTO = ToTourDTO(tour);
                    context.Tours.Add(tourDTO);
                    await context.SaveChangesAsync();
                    logger.Info($"Tour created successfully with ID: {tourDTO.Id}");
                    return tourDTO.Id;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while creating tour: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteTour(Tour tour)
        {
            logger.Info($"Deleting tour with ID: {tour.Id}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    TourDTO existingTour = await context.Tours.FindAsync(tour.Id);

                    if (existingTour != null)
                    {
                        context.Tours.Remove(existingTour);
                        await context.SaveChangesAsync();
                        logger.Info($"Tour with ID {tour.Id} deleted successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while deleting tour: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Tour>> GetAllTours()
        {
            logger.Info("Fetching all tours.");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    IEnumerable<TourDTO> tourDTOs = await context.Tours.ToListAsync();

                    logger.Info("Tours fetched successfully.");
                    return tourDTOs.Select(ToTour);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while fetching all tours: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateTour(Tour tour)
        {
            logger.Info($"Updating tour with ID: {tour.Id}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    TourDTO existingTour = await context.Tours.FindAsync(tour.Id);

                    if (existingTour != null)
                    {
                        existingTour.Name = tour.Name;
                        existingTour.Description = tour.Description;
                        existingTour.From = tour.From;
                        existingTour.To = tour.To;
                        existingTour.TransportType = tour.TransportType;
                        existingTour.TourDistance = tour.TourDistance;
                        existingTour.EstimatedTime = tour.EstimatedTime;

                        await context.SaveChangesAsync();
                        logger.Info($"Tour with ID {tour.Id} updated successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while updating tour: {ex.Message}");
                throw;
            }
        }
    }
}
