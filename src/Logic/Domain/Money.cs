using Logic.Domain.Common;

namespace Logic.Domain;

public sealed class Money : ValueObject<Money>
{
    public static readonly Money None = new(0, 0, 0, 0, 0, 0);
    public static readonly Money Cent = new(1, 0, 0, 0, 0, 0);
    public static readonly Money TenCent = new(0, 1, 0, 0, 0, 0);
    public static readonly Money Quarter = new(0, 0, 1, 0, 0, 0);
    public static readonly Money Dollar = new(0, 0, 0, 1, 0, 0);
    public static readonly Money FiveDollar = new(0, 0, 0, 0, 1, 0);
    public static readonly Money TwentyDollar = new(0, 0, 0, 0, 0, 1);

    public int OneCentCount { get; }
    public int TenCentCount { get; }
    public int QuarterCentCount { get; }
    public int OneDollarCount { get; }
    public int FiveDollarCount { get; }
    public int TwentyDollarCount { get; }

    public decimal Amount => OneCentCount * 0.01m + TenCentCount * 0.1m + QuarterCentCount * 0.25m + OneDollarCount + FiveDollarCount * 5 + TwentyDollarCount * 20;

    public Money() { }
    
    public Money(int oneCentCount, int tenCentCount, int quarterCentCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount) : this()
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
            var hashcode = OneCentCount;

            hashcode = (hashcode * 397) ^ TenCentCount;
            hashcode = (hashcode * 397) ^ QuarterCentCount;
            hashcode = (hashcode * 397) ^ OneDollarCount;
            hashcode = (hashcode * 397) ^ FiveDollarCount;
            hashcode = (hashcode * 397) ^ TwentyDollarCount;

            return hashcode;
        }
    }

    public Money Allocate(decimal amount)
    {
        if (!CanAllocate(amount))
            throw new InvalidOperationException();

        return AllocateCore(amount);
    }

    public bool CanAllocate(decimal amount)
    {
        var money = AllocateCore(amount);
        return money.Amount == amount;
    }

    /*
     * We try to retain small coins and notes
     */
    private Money AllocateCore(decimal amount)
    {
        var twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
        amount -= twentyDollarCount * 20;

        var fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
        amount -= fiveDollarCount * 5;

        var oneDollarCount = Math.Min((int)amount, OneDollarCount);
        amount -= oneDollarCount;

        var quarterCount = Math.Min((int)(amount / 0.25m), QuarterCentCount);
        amount -= quarterCount * 0.25m;

        var tenCentCount = Math.Min((int)(amount / 0.1m), TenCentCount);
        amount -= tenCentCount * 0.1m;

        var oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);

        return new Money(
            oneCentCount,
            tenCentCount,
            quarterCount,
            oneDollarCount,
            fiveDollarCount,
            twentyDollarCount);
    }

    public static Money operator *(Money money1, int multiplier) =>
        new(
            money1.OneCentCount * multiplier,
            money1.TenCentCount * multiplier,
            money1.QuarterCentCount * multiplier,
            money1.OneDollarCount * multiplier,
            money1.FiveDollarCount * multiplier,
            money1.TwentyDollarCount * multiplier);

    public override string ToString()
    {
        if (Amount < 1)
            return "¢" + (Amount * 100).ToString("0");

        return "$" + Amount.ToString("0.00");
    }
}