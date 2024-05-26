using Microsoft.EntityFrameworkCore;
using TourPlanner.DAL.Dtos;

namespace TourPlanner.DAL.DbContexts
{
    public class TourPlannerDbContext : DbContext
    {
        public DbSet<TourDTO> Tours { get; set; }
        public DbSet<TourLogDTO> TourLogs { get; set; }

        public TourPlannerDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
