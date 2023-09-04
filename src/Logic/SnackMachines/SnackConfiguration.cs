#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Logic.SnackMachines;

public class SnackConfiguration : IEntityTypeConfiguration<Snack>
{
    public void Configure(EntityTypeBuilder<Snack> entity)
    {
        entity.ToTable("Snack", "dbo");
        entity.HasKey(e => e.Id);
        entity.Property(s => s.Id).HasColumnName("SnackId");
        entity.Property(e => e.Id).HasColumnType("bigint");
    }
}