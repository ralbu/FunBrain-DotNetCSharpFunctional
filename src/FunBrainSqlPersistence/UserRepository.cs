using FunBrainDomain;
using FunBrainInfrastructure;
using FunBrainInfrastructure.Models;
using Functional;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using FunBrainInfrastructure.Repositories;
using Unit = System.ValueTuple;
using static Functional.F;

namespace FunBrainSqlPersistence
{
    public class UserRepository : IUserRepository
    {

            string cnString = "Data Source=.;Initial Catalog=FunBrain;Integrated Security=True;";

        public IList<User> GetAll()
        {
            var users = new List<User>();

            string sql = "select * from Users";
            using (var cn = new SqlConnection(cnString))
            {
                users = cn.Query<User>(sql).ToList();
            }

            return users;
        }
        
        public Option<User> GetUserBy(int id)
        {
            User user;
            var sql = "select * from Users where Id = @id";
            using (var cn = new SqlConnection(cnString))
            {
                // TODO: First or default need to be fixed
                user = cn.Query<User>(sql, new {id = id}).FirstOrDefault();
            }

            return user;
        }

        public Either<Error, User> Create(UserCreate newUser)
        {
            if (string.IsNullOrEmpty(newUser.Email))
            {
                return Errors.UserEmailEmpty;
            }
            if (string.IsNullOrEmpty(newUser.Name))
            {
                return Errors.UserNameEmpty;
            }

            var sql = "insert Users Values (@name, @email); SELECT CAST(SCOPE_IDENTITY() as int) ";

            var userId = 0;
            using (var cn = new SqlConnection(cnString))
            {
                userId = cn.QuerySingle<int>(sql, new { Name = newUser.Name, email = newUser.Email});
            }

            return new User
            {
                Id = userId,
                Name = newUser.Name,
                Email = newUser.Email
            };
        }

        public User Update(UserUpdate updateUser) { return null; }

        public Either<Error, Unit> Delete(int userId)
        {
            var sql = "delete from Users where id = @Id";
            using (var cn = new SqlConnection(cnString))
            {
                var res = cn.Execute(sql, new { Id = userId});
            }
            // response 1 = deleted

            throw new NotImplementedException();
        }

        public Option<bool> DeleteWithOption(int userId)
        {
            var sql = "delete from Users where id = @Id";
            using (var cn = new SqlConnection(cnString))
            {
                var res = cn.Execute(sql, new { Id = userId});
                if (res == 0)
                {
                    return None;
                }
                else
                {
                    return Some(true);
                }
            }
        }



        private Either<Error, Unit> ValidateUserUsedInGame(int userId)
        {
            if (userId == 1)
            {
                return Errors.UserUsedInGame;
            }

            return Unit();
        }
        private Either<Error, Unit> ValidateUserIsAdministrator(int userId)
        {
            if (userId == 2)
            {
                return Errors.UserIsAdministrator;
            }

            return Unit();
        }
    }

}
