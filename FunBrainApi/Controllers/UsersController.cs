using FunBrainInfrastructure.Models;
using FunBrainInfrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FunBrainApi.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // TODO: can this be done differently?
        [HttpGet]
        public IActionResult GetUsers()
            => Ok(_userRepository.GetAll());


        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUserBy(int id)
            => _userRepository
                .GetUserBy(id)
                .Match<IActionResult>(
                    () => NotFound(),
                    user => Ok(user));


        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreate newUser)
            => _userRepository.Create(newUser)
                .Match<IActionResult>(
                    a => BadRequest(a),
                    user => CreatedAtRoute("GetUser", new {id = user.Id}, user));

        //TODO: this needs a different approach
        [HttpPost]
        public IActionResult UpdateUser([FromBody] UserUpdate updateUser)
        {
            if (updateUser == null) // or user name or email is empty
            {
                return BadRequest();
            }
            if (updateUser.Id == 1) // not in the database
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _userRepository.DeleteWithOption(id).Match<IActionResult>(
                () => NotFound(),
                (val) => Ok());
    }

    public class ResultDto<T>
    {
        public bool Succeeded { get; }
        public bool Failed => !Succeeded;

        public T Data { get; }
        public Error Error { get; }

        public ResultDto(T data)
        {
            Succeeded = true;
            Data = data;
        }
        public ResultDto(Error error)
        {
            Error = error;
        }
    }
}