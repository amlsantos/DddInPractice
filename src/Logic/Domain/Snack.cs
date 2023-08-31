using Logic.Domain.Common;

namespace Logic.Domain;

public class Snack : AggregateRoot
{
    public static readonly Snack None = new Snack(0, nameof(None));
    public static readonly Snack Chocolate = new Snack(1, nameof(Chocolate));
    public static readonly Snack Soda = new Snack(2, nameof(Soda));
    public static readonly Snack Gum = new Snack(3, nameof(Gum));
    
    public string Name { get; protected set; }

    private Snack(long id, string name)
    {
        Id = id;
        Name = name;
    }
}