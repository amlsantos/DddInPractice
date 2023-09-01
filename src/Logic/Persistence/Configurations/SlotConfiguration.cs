using Logic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Persistence.Configurations;

public class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    public void Configure(EntityTypeBuilder<Slot> entity)
    {
        entity.ToTable("Slot", "dbo");
        entity.HasKey(e => e.Id);
        entity.Property(s => s.Id).HasColumnName("SlotId");
        
        var snackPile = entity.OwnsOne<SnackPile>(e => e.SnackPile);
        snackPile.Property(e => e.SnackId).HasColumnName("SnackId");
        snackPile.Property(e => e.Quantity).HasColumnName("Quantity");
        snackPile.Property(e => e.Price).HasColumnName("Price");

        snackPile.HasOne(s => s.Snack).WithOne();
    }
}