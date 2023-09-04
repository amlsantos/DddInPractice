#region

using Logic.Common;

#endregion

namespace Logic.SnackMachines;

public class Snack : AggregateRoot
{
    public static readonly Snack None = new(1, nameof(None));
    public static readonly Snack Chocolate = new(2, nameof(Chocolate));
    public static readonly Snack Soda = new(3, nameof(Soda));
    public static readonly Snack Gum = new(4, nameof(Gum));

    public Snack(string name)
    {
        Name = name;
    }

    private Snack(int id, string name) : this(name)
    {
        Id = id;
    }

    public string Name { get; protected set; }
}