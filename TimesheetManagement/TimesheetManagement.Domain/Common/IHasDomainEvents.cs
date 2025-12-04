using System.Collections.Generic;

namespace TimesheetManagement.Domain.Common;
public interface IHasDomainEvents
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}