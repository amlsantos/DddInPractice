﻿using Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SnackMachineConfiguration :IEntityTypeConfiguration<SnackMachine>
{
    public void Configure(EntityTypeBuilder<SnackMachine> entity)
    {
        entity.ToTable("SnackMachine", "dbo");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("SnackMachineID");
        
        var moneyInside = entity.OwnsOne<Money>(e => e.MoneyInside);
        moneyInside.Property(e => e.OneCentCount).HasColumnName("OneCentCount");
        moneyInside.Property(e => e.TenCentCount).HasColumnName("TenCentCount");
        moneyInside.Property(e => e.QuarterCentCount).HasColumnName("QuarterCount");
        moneyInside.Property(e => e.OneDollarCount).HasColumnName("OneDollarCount");
        moneyInside.Property(e => e.FiveDollarCount).HasColumnName("FiveDollarCount");
        moneyInside.Property(e => e.TwentyDollarCount).HasColumnName("TwentyDollarCount");
        
        entity.Ignore(e => e.MoneyInTransaction);
    }
}