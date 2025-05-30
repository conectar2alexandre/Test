using Test.DTO.Out.User;
using Test.DTO;
using Microsoft.AspNetCore.Identity;
using Test.Domain;
using Test.Interfaces.Repository;
using Test.Interfaces.Services;

namespace Test.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GenericResultDTO<UserDTO>> Get(string username)
        {
            var _result = new GenericResultDTO<UserDTO>();
            var _res = await _unitOfWork.UserRepository.Get(username);

            var _userDTO = new UserDTO()
            {
                Email = _res.Email,
                Login = _res.Login
            };

            _result.Content = _userDTO;

            return _result;
        }
    }
}
