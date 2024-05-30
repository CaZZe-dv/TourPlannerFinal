using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TourPlanner.DAL.DbContexts
{
    public class TourPlannerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TourPlannerDbContext>
    {
        public TourPlannerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TourPlannerDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5455;Username=postgres;Password=1234;Database=TourPlannerDatabase");

            return new TourPlannerDbContext(optionsBuilder.Options);
        }
    }
}
