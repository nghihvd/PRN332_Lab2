using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.Repositories;

namespace PRN232.Lab2.CoffeeStore.Repositories.Interfaces.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
