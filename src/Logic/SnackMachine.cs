namespace Logic;

using static Money;

public sealed class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; } = None; /* This money belongs to the machine */
    public Money MoneyInTransaction { get; private set; } = None; /* This money represents the current transaction */
    
    public void InsertMoney(Money money)
    {
        var coinsAndNotes = new[] { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        if (!coinsAndNotes.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money;
    }

    public void ReturnMoney() => MoneyInTransaction = None;

    public void BuySnack()
    {
        // put the money inside the machine
        MoneyInside += MoneyInTransaction;

        ReturnMoney();
    }
}