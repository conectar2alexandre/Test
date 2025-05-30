using Test.Domain;
using Test.Interfaces.Repository;
using Dapper;
namespace Test.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IContextAcessor contextAcessor) : base(contextAcessor)
        {
        }
        public async Task<UserEntity> Get(string username)
        {
            const string sql = @"SELECT Email, Login, PasswordHash FROM dbo.[Users] WHERE username = @username";

            var user = await Connection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { Login = username }, Transaction);

            if (user != null)
                return user;

            return null;
        }
    }
}
