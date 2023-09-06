#region

using Logic.SnackMachines;
using MediatR;

#endregion

namespace Logic.Common;

public abstract class Repository<T> : IDisposable where T : AggregateRoot
{
    private readonly IMediator _mediator;
    protected readonly ApplicationDbContext Context;

    public Repository(IMediator mediator, ApplicationDbContext context)
    {
        _mediator = mediator;
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
        await DispatchDomainEventsAsync();

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

    private async Task DispatchDomainEventsAsync()
    {
        var entitiesWithDomainEvents = Context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();

        if (entitiesWithDomainEvents.Count == 0)
            return;

        var domainEvents = entitiesWithDomainEvents
            .SelectMany(entry => entry.DomainEvents)
            .ToList();

        // clear domain events
        entitiesWithDomainEvents.ForEach(entity => entity.ClearEvents());

        // Dispatch
        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);
    }
}