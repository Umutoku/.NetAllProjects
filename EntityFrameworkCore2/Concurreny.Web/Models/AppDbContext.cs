using Microsoft.EntityFrameworkCore;

namespace Concurreny.Web.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion(); //Concurrency check

            modelBuilder.Entity<Product>().Property(x=>x.Price).HasColumnType("decimal(18,2)"); //Precision and scale



            base.OnModelCreating(modelBuilder);
        }
    }
}
