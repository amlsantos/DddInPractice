using Logic.Domain.Common;

namespace Logic.Domain;

public class Slot : Entity
{
    public long SnackMachineId { get; protected set; }
    public SnackMachine SnackMachine { get; protected set; }
    public SnackPile SnackPile { get; protected set; }
    public int Position { get; protected set; }
    
    public Slot(SnackMachine snackMachine, int position)
    {
        SnackMachineId = snackMachine?.Id ?? 0;
        SnackMachine = snackMachine;
        Position = position;
        SnackPile = SnackPile.Empty;
    }

    public void LoadSnack(SnackPile snackPile) => SnackPile = snackPile;

    public void DecreaseQuantity() => SnackPile = SnackPile.SubtractOne();

    public decimal ProductPrice() => SnackPile.Price;
}