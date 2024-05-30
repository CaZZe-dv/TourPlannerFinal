using Microsoft.EntityFrameworkCore;
using TourPlanner.BL.Models;
using TourPlanner.DAL.DbContexts;
using TourPlanner.DAL.Dtos;

namespace TourPlanner.DAL.Services
{
    public class DatabaseTour : DatabaseConverter, ITour
    {
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
        public DatabaseTour(TourPlannerDbContextFactory tourPlannerDbContextFactory)
        {
            _tourPlannerDbContextFactory = tourPlannerDbContextFactory;
        }

        public async Task<Guid> CreateTour(Tour tour)
        {
            using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
            {
                TourDTO tourDTO = ToTourDTO(tour);
                context.Tours.Add(tourDTO);
                await context.SaveChangesAsync();
                return tourDTO.Id;
            }
        }

        public async Task DeleteTour(Tour tour)
        {
            using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
            {
                TourDTO existingTour = await context.Tours.FindAsync(tour.Id);

                if (existingTour != null)
                {
                    context.Tours.Remove(existingTour);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Tour>> GetAllTours()
        {
            using (TourPlannerDbContext context = _tourPlannerDbContextFactory.CreateDbContext())
            {
                IEnumerable<TourDTO> tourDTOs = await context.Tours.ToListAsync();

                return tourDTOs.Select(ToTour);
            }
        }

        public async Task UpdateTour(Tour tour)
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
                }
            }
        }
    }
}
