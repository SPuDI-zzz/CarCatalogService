using CarCatalogService.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Data;

public class MainDbContext : IdentityDbContext<User, UserRole, long>
{
    public DbSet<Car> Cars { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<UserRole>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserToken<long>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<long>>().ToTable("user_role_owners");
        modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("user_claims");

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
