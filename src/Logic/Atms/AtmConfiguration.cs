using Logic.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.Atms;

public class AtmConfiguration : IEntityTypeConfiguration<Atm>
{
    public void Configure(EntityTypeBuilder<Atm> entity)
    {
        entity.ToTable("Atm", "dbo");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("AtmId");

        var moneyInside = entity.OwnsOne<Money>(e => e.MoneyInside);
        moneyInside.Property(e => e.OneCentCount).HasColumnName("OneCentCount");
        moneyInside.Property(e => e.TenCentCount).HasColumnName("TenCentCount");
        moneyInside.Property(e => e.QuarterCentCount).HasColumnName("QuarterCount");
        moneyInside.Property(e => e.OneDollarCount).HasColumnName("OneDollarCount");
        moneyInside.Property(e => e.FiveDollarCount).HasColumnName("FiveDollarCount");
        moneyInside.Property(e => e.TwentyDollarCount).HasColumnName("TwentyDollarCount");

        entity.Property(e => e.MoneyCharged).HasColumnName("MoneyCharged");

        entity.Ignore(e => e.DomainEvents);
    }
}