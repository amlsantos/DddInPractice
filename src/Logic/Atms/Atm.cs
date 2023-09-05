using Logic.Common;
using Logic.SharedKernel;
using static Logic.SharedKernel.Money;

namespace Logic.Atms;

public class Atm : AggregateRoot
{
    private const decimal CommissionRate = 0.01m;

    public virtual Money MoneyInside { get; protected set; } = None;
    public virtual decimal MoneyCharged { get; protected set; }

    public string CanTakeMoney(decimal amount)
    {
        if (amount < 0m)
            return "Invalid amount";

        if (MoneyInside.Amount < amount)
            return "Not enough money inside the Atm";

        if (!MoneyInside.CanAllocate(amount))
            return "Not enough change";

        return string.Empty;
    }

    public void TakeMoney(decimal amount)
    {
        var errors = CanTakeMoney(amount);
        if (!string.IsNullOrEmpty(errors))
            throw new InvalidOperationException();

        var output = MoneyInside.Allocate(amount);
        MoneyInside -= output;

        var amountWithCommission = CalculateAmountWithCommission(amount);
        MoneyCharged += amountWithCommission;
    }

    public decimal CalculateAmountWithCommission(decimal amount)
    {
        var commission = amount * CommissionRate;
        var lessThanCent = commission % 0.01m;

        if (lessThanCent > 0)
            commission = commission - lessThanCent + 0.01m;

        return amount + commission;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}