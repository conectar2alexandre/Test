using Microsoft.Data.SqlClient;
using System.Data;
using Test.Interfaces.Repository;

namespace Test.Repository
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString =  _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public async Task<IDbConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var _connection = new SqlConnection(_connectionString);
                await _connection.OpenAsync(cancellationToken);
                return _connection;
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while opening the Database connection.", ex);
            }
        }
    }
}
