namespace Logic;

public sealed class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; } /* This money belongs to the machine */
    public Money MoneyInTrasantion { get; private set; } /* This money represents the current transaction */

    public void InsertMoney(Money money)
    {
        MoneyInTrasantion += money;
    }

    /* To cancel the current transaction */

    public void ReturnMoney()
    {
        //MoneyInTrasantion = 0;
    }

    public void BuySnack()
    {
        // put the money inside the machine
        MoneyInside += MoneyInTrasantion;

        ReturnMoney();
    }
}