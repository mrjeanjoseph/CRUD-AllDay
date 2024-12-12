using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;

namespace AdventureWorks.Domain.Repository;

public interface IDataRepository : IDisposable {
    AdWDbContext Context { get; }

    IBaseRepository<Department> DepartmentRepository { get; }
}
