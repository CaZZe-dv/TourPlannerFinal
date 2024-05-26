using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TourPlanner.DAL.DbContexts
{
    public class TourPlannerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TourPlannerDbContext>
    {
        public TourPlannerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TourPlannerDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=12345;Database=TourPlannerDatabase");

            return new TourPlannerDbContext(optionsBuilder.Options);
        }
    }
}
