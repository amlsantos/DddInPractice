using Logic.Common;

namespace Logic.Atms;

public class BalanceChangedEvent : IDomainEvent
{
    public BalanceChangedEvent(long headOfficeId, decimal amount)
    {
        HeadOfficeId = headOfficeId;
        Amount = amount;
    }

    public long HeadOfficeId { get; private set; }
    public decimal Amount { get; private set; }
}