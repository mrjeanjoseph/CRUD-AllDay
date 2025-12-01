using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Application.Common.Abstractions;
public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}
