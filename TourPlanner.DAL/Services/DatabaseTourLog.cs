using Microsoft.EntityFrameworkCore;
using TourPlanner.BL.Models;
using TourPlanner.DAL.DbContexts;
using TourPlanner.DAL.Dtos;

namespace TourPlanner.DAL.Services
{
    public class DatabaseTourLog : DatabaseConverter, ITourLog
    {
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;

        public DatabaseTourLog(TourPlannerDbContextFactory tourplannerDbContextFactory)
        {
            _tourPlannerDbContextFactory = tourplannerDbContextFactory;
        }
        public async Task CreateTourLog(TourLog tourLog)
        {
            using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
            {
                context.TourLogs.Add(ToTourLogDTO(tourLog));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteTourLog(TourLog tourLog)
        {
            using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
            {
                TourLogDTO existingTourLog = await context.TourLogs.FindAsync(tourLog.Id);
                context.TourLogs.Remove(existingTourLog);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveAllTourLogs(Tour tour)
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
            }
        }


        public async Task<IEnumerable<TourLog>> GetAllTourLogs(Tour tour)
        {
            using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
            {
                IEnumerable<TourLogDTO> tourLogs = await context.TourLogs.Where(t => t.TourId == tour.Id).ToListAsync();

                return tourLogs.Select(t => ToTourLog(t));
            }
        }

        public async Task UpdateTourLog(TourLog tourLog)
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
                }
            }
        }
    }
}
