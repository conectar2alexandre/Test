using System.Data;

namespace Test.Interfaces.Repository
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken = default);
    }
}
