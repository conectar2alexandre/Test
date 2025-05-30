using System.Data;

namespace Test.Interfaces.Repository
{

    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository UserRepository { get; }
        Task InitializeAsync(CancellationToken ct = default);
        void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
