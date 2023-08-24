using Logic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Persistence.Configurations;

public class SnackConfiguration : IEntityTypeConfiguration<Snack>
{
    public void Configure(EntityTypeBuilder<Snack> entity)
    {
        entity.ToTable("Snack", "dbo");
        entity.Property(s => s.Id).HasColumnName("SnackId");
    }
}