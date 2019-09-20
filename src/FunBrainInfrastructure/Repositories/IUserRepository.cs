using System.Collections.Generic;
using FunBrainDomain;
using FunBrainInfrastructure.Models;
using Functional;

namespace FunBrainInfrastructure
{
    public interface IUserRepository
    {
        Option<IList<User>> Get();
        User GetById(int id);
        User Create(UserCreate newUser);
        User Update(UserUpdate updateUser);
        bool Delete(int userId);

        Either<Error, Unit> Delete(int userId);


    }

    public class Error
    {
        public virtual string Message { get; }
    }

    public sealed class UserUsedInGame: Error 
    {
        public override string Message { get; } = "Can't delete a user which is used in game";
    }


    public static class Errors
    {
        public static UserUsedInGame UserUsedInGame => new UserUsedInGame;
    }
}
