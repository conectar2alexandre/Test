using Test.Domain;

namespace Test.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<UserEntity> Get(string username);
    }
}
