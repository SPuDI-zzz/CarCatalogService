using CarCatalogService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Data;

public class MainDbContext : DbContext
{
    private readonly IConfiguration _config;
    public DbSet<Car> Cars { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public MainDbContext(DbContextOptions options, IConfiguration config) : base(options)
    {
        _config = config;
    }


    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var options = optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable(_config.GetConnectionString("DefaultConnection")!));
        base.OnConfiguring(options);
    }*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<Role>().Property(val => val.Name).IsRequired();
        modelBuilder.Entity<Role>().Property(val => val.Name).HasMaxLength(100);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(val => val.Login).IsRequired();
        modelBuilder.Entity<User>().Property(val => val.Login).HasMaxLength(50);
        modelBuilder.Entity<User>().Property(val => val.Password).IsRequired();
        modelBuilder.Entity<User>().Property(val => val.Password).HasMaxLength(50);
        modelBuilder.Entity<User>().HasOne(val => val.Role).WithMany(val => val.Users).HasForeignKey(val => val.RoleId);

        modelBuilder.Entity<Car>().ToTable("cars");
        modelBuilder.Entity<Car>().Property(val => val.Mark).IsRequired();
        modelBuilder.Entity<Car>().Property(val => val.Mark).HasMaxLength(100);
        modelBuilder.Entity<Car>().Property(val => val.Model).IsRequired();
        modelBuilder.Entity<Car>().Property(val => val.Model).HasMaxLength(100);
        modelBuilder.Entity<Car>().Property(val => val.Color).IsRequired();
        modelBuilder.Entity<Car>().Property(val => val.Color).HasMaxLength(100);
        modelBuilder.Entity<Car>().HasOne(val => val.User).WithMany(val => val.Cars).HasForeignKey(val => val.UserId);
    }
}
