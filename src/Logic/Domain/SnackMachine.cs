using Logic.Domain.Common;

namespace Logic.Domain;

using static Money;

public sealed class SnackMachine : AggregateRoot
{
    public const int MaxNumberSlots = 3;
    public Money MoneyInside { get; private set; } = None;
    public decimal MoneyInTransaction { get; private set; } = None.Amount;
    public IList<Slot> Slots { get; } = new List<Slot>(MaxNumberSlots);

    public void InsertMoney(Money money)
    {
        var coinsAndNotes = new[] { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        if (!coinsAndNotes.Contains(money))
            throw new InvalidOperationException();
        
        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public void LoadMoney(Money money) => MoneyInside += money;

    public void BuySnack(int position)
    {
        if (CanBuySnack(position) != string.Empty)
            throw new InvalidOperationException();

        var slot = GetSlot(position);
        slot.DecreaseProductQuantity();

        // we try to retain small coins and notes
        var moneyToReturn = MoneyInTransaction - slot.ProductPrice();
        var change = MoneyInside.Allocate(moneyToReturn);

        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    public string CanBuySnack(int position)
    {
        var snackPile = GetSnackPile(position);
        if (snackPile.Quantity == 0)
            return "The snack pile is empty";
        
        if (MoneyInTransaction < snackPile.Price)
            return "Not enough money";
        
        if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
            return "Not enough change";
        
        return string.Empty;
    }

    public SnackPile GetSnackPile(int position) => GetSlot(position).SnackPile;

    private Slot GetSlot(int position)
    {
        ValidateSlotPosition(position);

        return Slots.Single(s => s.Position == position);
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        ValidateSlotPosition(position);

        var slot = new Slot(this, position);
        slot.LoadSnack(snackPile);

        Slots.Add(slot);
    }

    private void ValidateSlotPosition(int candidatePosition)
    {
        var validPositions = Enumerable.Range(1, MaxNumberSlots).ToArray();
        if (!validPositions.Contains(candidatePosition))
            throw new InvalidOperationException();
    }

    public void ReturnMoney()
    {
        // we try to retain small coins and notes
        var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);

        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    public IEnumerable<SnackPile> GetAllSnackPiles()
    {
        return Slots
            .OrderBy(x => x.Position)
            .Select(x => x.SnackPile)
            .ToList();
    }
}