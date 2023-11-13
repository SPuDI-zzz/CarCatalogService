using CarCatalogService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Data;

public class MainDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public MainDbContext() : base() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<Role>().Property(val => val.Name).IsRequired();

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(val => val.Login).IsRequired();
        modelBuilder.Entity<User>().Property(val => val.Password).IsRequired();

        modelBuilder.Entity<Car>().ToTable("cars");
        modelBuilder.Entity<Car>().Property(val => val.Mark).IsRequired();
        modelBuilder.Entity<Car>().Property(val => val.Model).IsRequired();
        modelBuilder.Entity<Car>().Property(val => val.Color).IsRequired();
    }
}
