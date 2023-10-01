using Microsoft.EntityFrameworkCore;

namespace FluentValidationWeb.App.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
