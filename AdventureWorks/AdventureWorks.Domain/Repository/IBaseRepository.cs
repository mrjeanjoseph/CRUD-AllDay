namespace AdventureWorks.Domain.Repository;

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
