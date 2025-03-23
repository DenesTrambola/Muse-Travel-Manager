using Microsoft.EntityFrameworkCore;
using Muse_Travel_Manager.Models;

namespace Muse_Travel_Manager;

public class ApplicationDbContext : DbContext
{
    public DbSet<Tour> Tours { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Destination> Destinations { get; set; } = null!;

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=db.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasIndex(e => e.Phone)
            .IsUnique();

        modelBuilder.Entity<Client>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Destination>()
            .HasIndex(e => e.Name)
            .IsUnique();

        modelBuilder.Entity<Tour>()
            .Property(e => e.Price)
            .HasColumnType("REAL");
    }
}
