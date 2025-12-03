using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Application.Common.Abstractions;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
