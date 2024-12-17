using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Domain.Repository;

public class DataRepository : IDataRepository {

    #region Properties/Variables/Constructors
    public readonly AdWDbContext _context;
    private bool _disposed = false;

    public DataRepository(AdWDbContext context) {
        //_context.Database.EnsureCreated();
        _context = context;
        _context.Database.SetCommandTimeout(500);
        Context = context; // Initialize the Context field
    }
    #endregion

    public readonly AdWDbContext Context;
    AdWDbContext IDataRepository.Context => _context;  //Why? not sure yet

    public IBaseRepository<Department> DepartmentRepository => new BaseRepository<Department>(_context);


    #region Disposing the context
    //Still a good practice to dispose of the context
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
    #endregion
}
