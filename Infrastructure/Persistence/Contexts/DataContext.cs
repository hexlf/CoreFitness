
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class DataContext : DbContext
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
