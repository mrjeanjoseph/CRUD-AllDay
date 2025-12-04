using Microsoft.Extensions.Logging;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Infrastructure.Services;
public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(ILogger<DomainEventDispatcher> logger)
    {
        _logger = logger;
    }

    public Task DispatchAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("Domain event dispatched: {EventType}", domainEvent.GetType().Name);
        }
        return Task.CompletedTask;
    }
}