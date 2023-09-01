using Logic.Domain.Common;

namespace Logic.Domain;

public class Snack : AggregateRoot
{
    public static readonly Snack None = new(1, nameof(None));
    public static readonly Snack Chocolate = new(2, nameof(Chocolate));
    public static readonly Snack Soda = new(3, nameof(Soda));
    public static readonly Snack Gum = new(4, nameof(Gum));
    
    public string Name { get; protected set; }

    public Snack(string name) => Name = name;

    public Snack(int id, string name) : this(name) => Id = id;
}