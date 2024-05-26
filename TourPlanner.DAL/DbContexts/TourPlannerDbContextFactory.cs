using Microsoft.EntityFrameworkCore;

namespace TourPlanner.DAL.DbContexts
{
    public class TourPlannerDbContextFactory
    {
        private readonly string _connectionString;

        public TourPlannerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TourPlannerDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TourPlannerDbContext>();
            optionsBuilder.UseNpgsql(_connectionString);

            return new TourPlannerDbContext(optionsBuilder.Options);
        }
    }
}
