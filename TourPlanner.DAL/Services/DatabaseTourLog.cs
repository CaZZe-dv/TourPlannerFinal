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
    public class DatabaseTourLog : DatabaseConverter, ITourLog
    {
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public DatabaseTourLog(TourPlannerDbContextFactory tourplannerDbContextFactory)
        {
            _tourPlannerDbContextFactory = tourplannerDbContextFactory;
        }

        public async Task CreateTourLog(TourLog tourLog)
        {
            logger.Info($"Creating tour log for Tour ID: {tourLog.TourId}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    context.TourLogs.Add(ToTourLogDTO(tourLog));
                    await context.SaveChangesAsync();
                    logger.Info($"Tour log created successfully for Tour ID: {tourLog.TourId}");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while creating tour log: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteTourLog(TourLog tourLog)
        {
            logger.Info($"Deleting tour log with ID: {tourLog.Id}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    TourLogDTO existingTourLog = await context.TourLogs.FindAsync(tourLog.Id);
                    context.TourLogs.Remove(existingTourLog);
                    await context.SaveChangesAsync();
                    logger.Info($"Tour log with ID {tourLog.Id} deleted successfully");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while deleting tour log: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveAllTourLogs(Tour tour)
        {
            logger.Info($"Removing all tour logs for Tour ID: {tour.Id}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    IEnumerable<TourLogDTO> tourLogs = await context.TourLogs.Where(t => t.TourId == tour.Id).ToListAsync();

                    if (tourLogs != null && tourLogs.Any())
                    {
                        foreach (TourLogDTO tourLog in tourLogs)
                        {
                            context.TourLogs.Remove(tourLog);
                        }
                    }

                    await context.SaveChangesAsync();
                    logger.Info($"All tour logs removed successfully for Tour ID: {tour.Id}");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while removing all tour logs: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TourLog>> GetAllTourLogs(Tour tour)
        {
            logger.Info($"Fetching all tour logs for Tour ID: {tour.Id}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    IEnumerable<TourLogDTO> tourLogs = await context.TourLogs.Where(t => t.TourId == tour.Id).ToListAsync();
                    logger.Info($"All tour logs fetched successfully for Tour ID: {tour.Id}");
                    return tourLogs.Select(t => ToTourLog(t));
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while fetching all tour logs: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateTourLog(TourLog tourLog)
        {
            logger.Info($"Updating tour log with ID: {tourLog.Id}");
            try
            {
                using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    TourLogDTO existingTourLog = await context.TourLogs.FindAsync(tourLog.Id);

                    if (existingTourLog != null)
                    {
                        existingTourLog.DateTime = tourLog.DateTime.ToString();
                        existingTourLog.Comment = tourLog.Comment;
                        existingTourLog.Difficulty = tourLog.Difficulty;
                        existingTourLog.TotalDistance = tourLog.TotalDistance;
                        existingTourLog.TotalTime = tourLog.TotalTime;
                        existingTourLog.Rating = tourLog.Rating;

                        await context.SaveChangesAsync();
                        logger.Info($"Tour log with ID {tourLog.Id} updated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while updating tour log: {ex.Message}");
                throw;
            }
        }
    }
}
