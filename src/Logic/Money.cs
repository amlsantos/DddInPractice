namespace Logic;

public sealed class Money : ValueObject<Money>
{
    public int OneCentCount { get; private set; }
    public int TenCentCount { get; private set; }
    public int QuarterCentCount { get; private set; }
    public int OneDollarCount { get; private set; }
    public int FiveDollarCount { get; private set; }
    public int TwentyDollarCount { get; private set; }

    public Money(int oneCentCount, int tenCentCount, int quarterCentCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
    {
        OneCentCount = oneCentCount;
        TenCentCount = tenCentCount;
        QuarterCentCount = quarterCentCount;
        OneDollarCount = oneDollarCount;
        FiveDollarCount = fiveDollarCount;
        TwentyDollarCount = twentyDollarCount;
    }

    public static Money operator +(Money first, Money second)
    {
        return new Money(
            oneCentCount: first.OneCentCount + second.OneCentCount,
            tenCentCount: first.TenCentCount + second.TenCentCount,
            quarterCentCount: first.QuarterCentCount + second.QuarterCentCount,
            oneDollarCount: first.OneDollarCount + second.OneDollarCount,
            fiveDollarCount: first.FiveDollarCount + second.FiveDollarCount,
            twentyDollarCount: first.TwentyDollarCount + second.TwentyDollarCount
            );
    }

    protected override bool EqualsCore(Money other)
    {
        return OneCentCount == other.OneCentCount &&
            TenCentCount == other.TenCentCount &&
            QuarterCentCount == other.QuarterCentCount &&
            OneDollarCount == other.OneDollarCount &&
            FiveDollarCount == other.FiveDollarCount &&
            TwentyDollarCount == other.TwentyDollarCount;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashcode = OneCentCount;

            hashcode = (hashcode * 397) ^ TenCentCount;
            hashcode = (hashcode * 397) ^ QuarterCentCount;
            hashcode = (hashcode * 397) ^ OneDollarCount;
            hashcode = (hashcode * 397) ^ FiveDollarCount;
            hashcode = (hashcode * 397) ^ TwentyDollarCount;

            return hashcode;
        }
    }
}