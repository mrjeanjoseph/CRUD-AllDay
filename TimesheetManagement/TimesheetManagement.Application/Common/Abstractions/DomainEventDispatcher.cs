namespace TimesheetManagement.Application.Common.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default);
}
