using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private readonly IDomainEventDispatcher _dispatcher;

    public UnitOfWork(AppDbContext db, IDomainEventDispatcher dispatcher)
    {
        _db = db;
        _dispatcher = dispatcher;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = CollectDomainEvents();
        var result = await _db.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(domainEvents, cancellationToken);
        return result;
    }

    private List<IDomainEvent> CollectDomainEvents()
    {
        var domainEvents = new List<IDomainEvent>();
        foreach (var entry in _db.ChangeTracker.Entries<Entity>())
        {
            if (entry.Entity is IHasDomainEvents entityWithEvents)
            {
                domainEvents.AddRange(entityWithEvents.DomainEvents);
                entityWithEvents.ClearDomainEvents();
            }
        }
        return domainEvents;
    }

    private Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        => _dispatcher.DispatchAsync(domainEvents, cancellationToken);
}
