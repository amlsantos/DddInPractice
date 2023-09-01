using Logic.Domain.Common;

namespace Logic.Domain;

public class SnackPile : ValueObject<SnackPile>
{
    public static readonly SnackPile Empty = new(Snack.None, 0, 0m);
    
    public long SnackId { get; init; }
    public virtual Snack Snack { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    private SnackPile() { }
    
    public SnackPile(Snack snack, int? quantity, decimal? price) : this()
    {
        if (quantity < 0)
            throw new InvalidOperationException();
        if (price < 0)
            throw new InvalidOperationException();
        if (price % 0.01m > 0)
            throw new InvalidOperationException();
        
        SnackId = snack?.Id ?? 0;
        Snack = snack;
        Quantity = quantity ?? 0;
        Price = price ?? 0;
    }

    public SnackPile SubtractOne() => new(Snack, Quantity - 1, Price);

    protected override bool EqualsCore(SnackPile other) => 
        Snack == other.Snack && 
        Quantity == other.Quantity && 
        Price == other.Quantity;

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            var hashCode = Snack.GetHashCode();
            hashCode = (hashCode * 397) ^ Quantity;
            hashCode = (hashCode * 397) ^ Price.GetHashCode();

            return hashCode;
        }
    }
}