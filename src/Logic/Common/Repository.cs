using Logic.SnackMachines;

namespace Logic.Common;

public abstract class Repository<T> : IDisposable where T : AggregateRoot
{
    protected readonly ApplicationDbContext Context;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    public virtual T? GetById(long id)
    {
        return Context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        ValidateSlots();

        return await Context.SaveChangesAsync();
    }

    private void ValidateSlots()
    {
        var slots = Context.ChangeTracker.Entries()
            .Where(x => x.Entity is Slot);

        foreach (var slot in slots)
        {
            var entity = slot.Entity as Slot;
            var snackPile = entity?.SnackPile;

            if (snackPile is null)
                throw new InvalidOperationException();
        }
    }
}