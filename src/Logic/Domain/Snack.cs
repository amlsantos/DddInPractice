using Logic.Domain.Common;

namespace Logic.Domain;

public class Snack : AggregateRoot
{
    public string Name { get; protected set; }

    public Snack(string name) => Name = name;
}