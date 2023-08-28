using Logic.Domain.Common;

namespace Logic.Domain;

using static Money;

public sealed class SnackMachine : AggregateRoot
{
    public Money MoneyInside { get; private set; }
    public decimal MoneyInTransaction { get; private set; }
    protected IList<Slot> Slots { get; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = 0;
        Slots = new List<Slot>()
        {
            new(this, position:1),
            new(this, position:2),
            new(this, position:3),
        };
    }
    
    public void InsertMoney(Money money)
    {
        var coinsAndNotes = new[] { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        if (!coinsAndNotes.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public void LoadMoney(Money money) => MoneyInside = money;

    public void BuySnack(int position)
    {
        // decrease the quantity
        var slot = GetSlot(position);
        if (!InsertedMoneySufficient(slot))
            throw new InvalidOperationException();
        
        slot.DecreaseQuantity();

        var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
        if (!EnoughChange(change, slot))
            throw new InvalidOperationException();

        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    private bool InsertedMoneySufficient(Slot slot) => slot.SnackPile.Price <= MoneyInTransaction;

    private bool EnoughChange(Money change, Slot slot) => change.Amount >= MoneyInTransaction - slot.SnackPile.Price;

    public void ReturnMoney()
    {
        // we try to retain small coins and notes
        var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        
        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    public SnackPile GetSnackPile(int position) => GetSlot(position).SnackPile;

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        var slot = GetSlot(position);
        slot.LoadSnack(snackPile);
    }

    private Slot GetSlot(int position) => Slots.Single(s => s.Position == position);
}