using Microsoft.EntityFrameworkCore;
using WebApplication.EfStuff.Models;

namespace WebApplication.EfStuff
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions options):base(options) { }

        private DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}