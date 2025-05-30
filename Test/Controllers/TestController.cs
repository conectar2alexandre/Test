using Microsoft.AspNetCore.Mvc;
using Test.DTO.Out.User;
using Test.DTO;
using Test.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<GenericResultDTO<UserDTO>>> Get(string username)
        {
            var _genericResult = await _userService.Get(username);

            return Ok(_genericResult);
        }
    }
}
