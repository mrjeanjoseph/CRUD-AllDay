namespace AdventureWorks.Domain.Interface
{
    internal interface IAdWDbContext : IDisposable
    {
        int SaveOrSubmitChanges();
        void EditedOrUpdated(object entity);
        void AddOrInsert(object entity);
    }
}
