namespace Logic;

public sealed class Money : ValueObject<Money>
{
    public int OneCentCount { get; }
    public int TenCentCount { get; }
    public int QuarterCentCount { get; }
    public int OneDollarCount { get; }
    public int FiveDollarCount { get; }
    public int TwentyDollarCount { get; }

    public decimal Amount
    {
        get
        {
            return OneCentCount * 0.01m + TenCentCount * 0.1m + QuarterCentCount * 0.25m + OneDollarCount + FiveDollarCount * 5 + TwentyDollarCount * 20;
        }
    }

    public Money(int oneCentCount, int tenCentCount, int quarterCentCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
    {
        if (oneCentCount < 0)
            throw new InvalidOperationException();
        if (tenCentCount < 0)
            throw new InvalidOperationException();
        if (quarterCentCount < 0)
            throw new InvalidOperationException();
        if (oneDollarCount < 0)
            throw new InvalidOperationException();
        if (fiveDollarCount < 0)
            throw new InvalidOperationException();
        if (twentyDollarCount < 0)
            throw new InvalidOperationException();

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

    public static Money operator -(Money first, Money second)
    {
        return new Money(
            oneCentCount: first.OneCentCount - second.OneCentCount,
            tenCentCount: first.TenCentCount - second.TenCentCount,
            quarterCentCount: first.QuarterCentCount - second.QuarterCentCount,
            oneDollarCount: first.OneDollarCount - second.OneDollarCount,
            fiveDollarCount: first.FiveDollarCount - second.FiveDollarCount,
            twentyDollarCount: first.TwentyDollarCount - second.TwentyDollarCount
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