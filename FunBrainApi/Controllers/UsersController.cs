using FunBrainInfrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FunBrainApi.Controllers
{

    [Route("api/users")]
    public class UsersController: Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
            => _userRepository.Get()
                .Match<IActionResult>(
                    () => NotFound(),
                    (result) => Ok(result));


        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUserById(int id)
        {
            return Ok(new { user = "user" });
        }

    }
}
