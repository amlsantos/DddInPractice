using Logic.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Management;

public class HeadOfficeConfiguration : IEntityTypeConfiguration<HeadOffice>
{
    public void Configure(EntityTypeBuilder<HeadOffice> entity)
    {
        entity.ToTable("HeadOffice", "dbo");

        entity.HasKey(e => e.Id);
        entity.Property(s => s.Id).HasColumnName("HeadOfficeId");

        entity.Property(e => e.Balance).HasColumnName("Balance");

        var cash = entity.OwnsOne<Money>(e => e.Cash);
        cash.Property(e => e.OneCentCount).HasColumnName("OneCentCount");
        cash.Property(e => e.TenCentCount).HasColumnName("TenCentCount");
        cash.Property(e => e.QuarterCentCount).HasColumnName("QuarterCount");
        cash.Property(e => e.OneDollarCount).HasColumnName("OneDollarCount");
        cash.Property(e => e.FiveDollarCount).HasColumnName("FiveDollarCount");
        cash.Property(e => e.TwentyDollarCount).HasColumnName("TwentyDollarCount");
        
        entity.Ignore(e => e.DomainEvents);
    }
}