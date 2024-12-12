using AdventureWorks.Domain.DataAccessLayer;
using System.Linq;

namespace AdventureWorks.Domain.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class {
    private readonly AdWDbContext _context;

    public BaseRepository(AdWDbContext context) {
        _context = context;
    }

    public IQueryable<TEntity> GetAll() {
        return _context.Set<TEntity>();
    }

    public TEntity AddOrInsert(TEntity entity) {
        return _context.Set<TEntity>().Add(entity).Entity;
    }

    public IEnumerable<TEntity> AddOrInsertMultiple(IEnumerable<TEntity> entities) {
        _context.Set<TEntity>().AddRange(entities);
        return entities;
    }

    public void Remove(TEntity entity) {
        _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveMultiple(IEnumerable<TEntity> entities) {
        _context.Set<TEntity>().RemoveRange(entities);
    }
}
