using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace PingYourPackage.Domain
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        readonly DbContext _entitiesContext;

        public EntityRepository(DbContext entitiesContext)
        {
            _entitiesContext = entitiesContext ?? throw new ArgumentNullException("entitiesContext");

            //Opt-in for a shorter implementation
            //if (entitiesContext == null) throw new ArgumentNullException("entitiesContext");
            //_entitiesContext = entitiesContext;
        }
        public virtual IQueryable<T> All
        {
            get => GetAll();
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entitiesContext.Set<T>();

            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _entitiesContext.Set<T>().Where(predicate);
        public virtual IQueryable<T> GetAll() => _entitiesContext.Set<T>();
        public T GetSingle(Guid key) => GetAll().FirstOrDefault(x => x.Key == key);
        public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector) => 
            Paginate(pageIndex, pageSize, keySelector, null);

        public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, 
            Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AllIncluding(includeProperties).OrderBy(keySelector);

            query = (predicate == null) ? query : query.Where(predicate);

            return query.ToPaginatedList(pageIndex, pageSize);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry entityEntry = _entitiesContext.Entry<T>(entity);
            _entitiesContext.Set<T>().Add(entity);
        }
        public virtual void Edit(T entity)
        {
            DbEntityEntry entityEntry = _entitiesContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entityEntry = _entitiesContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public void Save() => _entitiesContext.SaveChanges();
    }
}