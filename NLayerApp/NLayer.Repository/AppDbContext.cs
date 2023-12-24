using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;

namespace NLayer.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //modelBuilder.ApplyConfiguration(new ProductConfiguration()); tek tek tanımlamak istersek

        modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
        {
            Id = 1, ProductId = 1, Color = "Red", Height = 100, Width = 200
        });
        base.OnModelCreating(modelBuilder);
    }
    
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entityReference)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    {
                        Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                        entityReference.CreatedDate = DateTime.Now;
                        break;
                    }
                    case EntityState.Modified:
                    {
                        Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                        entityReference.UpdatedDate = DateTime.Now;
                        break;
                    }
                }
            }
        }
            
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entityReference)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    {
                        Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                        entityReference.CreatedDate = DateTime.Now;
                        break;
                    }
                    case EntityState.Modified:
                    {
                        Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                        entityReference.UpdatedDate = DateTime.Now;
                        break;
                    }
                }
            }
        }
            
        return base.SaveChangesAsync(cancellationToken);
    }
}