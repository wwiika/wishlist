using Microsoft.EntityFrameworkCore;
using WishList.Domain.Entities;

namespace WishList.Infrastructure.EntityFramework;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Gift> Gifts { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<Friend> Friends { get; set; }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly( typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}