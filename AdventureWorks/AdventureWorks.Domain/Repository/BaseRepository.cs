using AdventureWorks.Domain.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdventureWorks.Domain.Repository;

#region Base Interface
public interface IBaseRepository<TEntity> where TEntity : class {
    IQueryable<TEntity> GetAll();

    TEntity AddOrInsert(TEntity entity);

    IEnumerable<TEntity> AddOrInsertMultiple(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveMultiple(IEnumerable<TEntity> entities);


    //Task<IQueryable<TEntity>> GetAllAsync();
    //Task<TEntity> AddOrInsertAsync(TEntity entity);
    //Task<IEnumerable<TEntity>> AddOrInsertMultipleAsync(IEnumerable<TEntity> entities);
    //Task RemoveAsync(TEntity entity);
    //Task RemoveMultipleAsync(IEnumerable<TEntity> entities);
}

#endregion

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

    //------------------------------------
    //public async Task<IQueryable<TEntity>> GetAllAsync() {
    //    return await _context.Set<TEntity>().ToListAsync();
    //}

    //public Task<TEntity> AddOrInsertAsync(TEntity entity) {
    //    throw new NotImplementedException();
    //}

    //public Task<IEnumerable<TEntity>> AddOrInsertMultipleAsync(IEnumerable<TEntity> entities) {
    //    throw new NotImplementedException();
    //}

    //public Task RemoveAsync(TEntity entity) {
    //    throw new NotImplementedException();
    //}

    //public Task RemoveMultipleAsync(IEnumerable<TEntity> entities) {
    //    throw new NotImplementedException();
    //}
}
