using System.Collections.Generic;
using FunBrainDomain;
using FunBrainInfrastructure.Models;
using Functional;
using Unit = System.ValueTuple;

namespace FunBrainInfrastructure.Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        Option<User> GetUserBy(int id);

//        User CreateOld(UserCreate newUser);

        Either<Error, User> Create(UserCreate newUser);

        User Update(UserUpdate updateUser);
        bool DeleteOld(int userId);

        Either<Error, Unit> Delete(int userId);
        Option<bool> DeleteWithOption(int userId);
    }

    public class Error
    {
        public virtual string Message { get; }
    }

    public sealed class UserUsedInGame: Error 
    {
        public override string Message { get; } = "Can't delete a user which is used in game";
    }
    public sealed class UserIsAdministrator: Error 
    {
        public override string Message { get; } = "Can't delete a user which is Administrator";
    }

    public sealed class UserEmailEmpty : Error
    {
        public override string Message { get; } = "User email is empty";
    }
    public sealed class UserNameEmpty : Error
    {
        public override string Message { get; } = "User name is empty";
    }


    public static class Errors
    {
        public static UserUsedInGame UserUsedInGame => new UserUsedInGame();
        public static UserIsAdministrator UserIsAdministrator => new UserIsAdministrator();
        public static UserEmailEmpty UserEmailEmpty => new UserEmailEmpty();
        public static UserNameEmpty UserNameEmpty => new UserNameEmpty();
    }
}
