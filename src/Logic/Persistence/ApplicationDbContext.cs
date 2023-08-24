using System.Reflection;
using Logic.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logic.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Snack> Snacks { get; set; }
    public DbSet<SnackMachine> SnackMachines { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            throw new InvalidOperationException("Invalid connection string");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}