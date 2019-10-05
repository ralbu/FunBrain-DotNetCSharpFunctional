using System;
using FunBrainInfrastructure;
using FunBrainInfrastructure.Models;
using FunBrainInfrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Functional;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Unit = System.ValueTuple;
using static Functional.F;

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

        // TODO: can this be done differently?
        [HttpGet]
        public IActionResult GetUsers()
            => Ok(_userRepository.GetAll());

//        [HttpGet]
//        public IActionResult GetUsers()
//            => _userRepository.Get()
//                .Match<IActionResult>(
//                    () => NotFound(),
//                    (result) => Ok(result));


        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUserBy(int id)
            => _userRepository
                .GetUserBy(id)
                .Match<IActionResult>(
                    () => NotFound(),
                    user => Ok(user));


        [HttpPost]
        public IActionResult CreateUser(UserCreate newUser) 
        => _userRepository.Create(newUser)
            .Match<IActionResult>(
                a => BadRequest(a),
                (s) => Ok(s));


        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _userRepository.DeleteWithOption(id).Match<IActionResult>(
                () => NotFound(),
                (val) => Ok());

        /*
        public void Abc(int id)
        {
            var t = _userRepository.DeleteWithOption(id)
                .Map()
        }
*/

/*
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result0 = _userRepository.DeleteWithOption(id);

            var r = _userRepository.DeleteWithOption(id).Match(
                () => (StatusCodeResult)NotFound(),
                (val) => (StatusCodeResult) Ok());

            return r;
        }

*/
/*
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
            => _userRepository.Delete(id)
                .Match<IActionResult>(
                Right: _ => Ok(),
                Left: BadRequest);
*/


            //=> ValidateUserUsedInGame(id)
             //   .Bind(_userRepository.Delete);

        private Either<Error, Unit> ValidateUserUsedInGame(int userId)
        {
            if (userId == 1)
            {
                return Errors.UserUsedInGame;
            }

            return Unit();
        }


    }
}
