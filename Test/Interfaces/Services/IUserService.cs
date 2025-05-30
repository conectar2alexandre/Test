using Test.DTO;
using Test.DTO.Out.User;

namespace Test.Interfaces.Services
{
    public interface IUserService
    {
        public Task<GenericResultDTO<UserDTO>> Get(string username);
    }
}
