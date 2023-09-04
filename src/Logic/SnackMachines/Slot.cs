#region

using Logic.Common;

#endregion

namespace Logic.SnackMachines;

public class Slot : Entity
{
    private Slot()
    {
        SnackPile = SnackPile.Empty;
    }

    public Slot(SnackMachine snackMachine, int position) : this()
    {
        SnackMachineId = snackMachine?.Id ?? 0;
        SnackMachine = snackMachine;
        Position = position;
    }

    public long SnackMachineId { get; }
    public virtual SnackMachine SnackMachine { get; }
    public int Position { get; protected set; }
    public SnackPile SnackPile { get; protected set; }

    public void LoadSnack(SnackPile snackPile)
    {
        SnackPile = snackPile;
    }

    public void DecreaseProductQuantity()
    {
        SnackPile = SnackPile.SubtractOne();
    }

    public decimal ProductPrice()
    {
        return SnackPile.Price;
    }
}