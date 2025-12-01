using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Application.Common.Abstractions;
public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}
