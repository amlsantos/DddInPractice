#region

using System.Reflection;
using Logic.Atms;
using Logic.Management;
using Logic.SnackMachines;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Logic.Common;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Snack> Snacks { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Atm> Atms { get; set; }
    public DbSet<HeadOffice> HeadOffices { get; set; }
    public DbSet<SnackMachine> SnackMachines { get; set; }

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