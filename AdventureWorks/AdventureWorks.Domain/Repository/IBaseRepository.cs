namespace AdventureWorks.Domain.Repository;

public interface IBaseRepository<TEntity> where TEntity : class {
    IQueryable<TEntity> GetAll();

    TEntity AddOrInsert(TEntity entity);

    IEnumerable<TEntity> AddOrInsertMultiple(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveMultiple(IEnumerable<TEntity> entities);
}
