using Microsoft.EntityFrameworkCore;
using NewsPortal.Entity;

namespace NewsPortal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<NewsEntity> News { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<RegisterUser> RegisterUsers { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
