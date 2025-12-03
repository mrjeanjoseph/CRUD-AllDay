using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Application.Common.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default);
}

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var @event in domainEvents)
        {
            Console.WriteLine($"Domain event dispatched: {@event.GetType().Name} at {DateTime.UtcNow}");
        }
        return Task.CompletedTask;
    }
}
