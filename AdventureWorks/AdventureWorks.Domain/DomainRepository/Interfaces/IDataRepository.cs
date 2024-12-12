using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;

namespace AdventureWorks.DomainRepository {
    public interface IDataRepository : IDisposable {
        AdWDbContext Context { get; }

        IBaseRepository<Department> DepartmentRepository { get; }
    }
}
