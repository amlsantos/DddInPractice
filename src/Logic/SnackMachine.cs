namespace Logic;

using static Logic.Money;

public sealed class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; } /* This money belongs to the machine */
    public Money MoneyInTransaction { get; private set; } /* This money represents the current transaction */

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = None;
    }

    public void InsertMoney(Money money)
    {
        var coinsAndNotes = new Money[] { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
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