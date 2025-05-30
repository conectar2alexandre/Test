namespace Test.Repository
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Test.Interfaces.Repository;
    using System.Data;
    using System.Xml;
    using Test.Interfaces.Repository;


        public class UnitOfWork : IUnitOfWork
        {
            private readonly IConfiguration _configuration;
            private readonly IDbConnectionFactory _dbConnectionFactory;
            private readonly IContextAcessor _contextAcessor;

            private IDbConnection? _connection;
            private IDbTransaction? _transaction;
            private List<int> _transactionsOpened = new();


            public IUserRepository UserRepository { get; }

            public IDbConnection Connection => _connection!;
            public IDbTransaction? Transaction => _transaction;

            public UnitOfWork(IConfiguration configuration, IDbConnectionFactory DbConnectionFactory,
                IContextAcessor contextAccessor,  IUserRepository userRepository)
            {
                _configuration = configuration;
                _dbConnectionFactory = DbConnectionFactory;
                _contextAcessor = contextAccessor;

                UserRepository = userRepository;
            }

            public async Task InitializeAsync(CancellationToken cancellationToken = default)
            {
                if (_connection == null || _connection.State != ConnectionState.Open)
                {
                    _connection = await _dbConnectionFactory.CreateOpenConnectionAsync(cancellationToken);
                    _contextAcessor.Connection = _connection;
                }
            }

            public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
            {
                if (_transactionsOpened.Count == 0)
                {
                    _transaction = _connection?.BeginTransaction(isolation);
                    _contextAcessor.Transaction = _transaction;
                }

                _transactionsOpened.Add(_transactionsOpened.Count + 1);
            }

            public void CommitTransaction()
            {
                if (_transaction != null && _transactionsOpened.Count == 1)
                {
                    _transaction?.Commit();
                    _contextAcessor.Transaction = _transaction;
                }

                if (_transactionsOpened.Count > 0)
                    _transactionsOpened.RemoveAt(_transactionsOpened.Count - 1);
            }

            public void RollbackTransaction()
            {
                if (_transaction != null && _transactionsOpened.Count > 0)
                {
                    _transaction?.Rollback();
                    _contextAcessor.Transaction = _transaction;
                }

                _transactionsOpened.Clear();
            }

            public async ValueTask DisposeAsync()
            {
                _transaction?.Dispose();
                _contextAcessor.Transaction = _transaction;

                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    if (_connection is SqlConnection sqlConn)
                        await sqlConn.DisposeAsync();
                    else
                        _connection?.Dispose();

                    _contextAcessor.Connection = _connection!;
                }

                _transactionsOpened.Clear();
            }
        }


}
