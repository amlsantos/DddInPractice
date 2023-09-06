using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Logic.Common;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public PublishDomainEventsInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();

        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        await DispatchDomainEventsAsync(eventData.Context);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(DbContext? dbContext)
    {
        var entitiesWithDomainEvents = dbContext?.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();

        if (entitiesWithDomainEvents is null || entitiesWithDomainEvents.Count == 0)
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