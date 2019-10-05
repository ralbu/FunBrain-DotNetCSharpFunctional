using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunBrainDomain;
using FunBrainInfrastructure.Models;
using Functional;
using Functional.Option;
using Unit = System.ValueTuple;
using static Functional.F;

namespace FunBrainInfrastructure.Repositories
{
    public class UserRepositoryInMemory //: IUserRepository
    {
        public IList<User> Users { get; set; } = new List<User>();

        public UserRepositoryInMemory()
        {
            Users.Add(new User
            {
                Id = 1,
                Name = "User 1",
                Email = "user1@email.com",

            });
            Users.Add(new User
            {
                Id = 2,
                Name = "User 2",
                Email = "user2@email.com"
            });
        }

        public Option<IList<User>> Get()
            => Some(Users);


        public User GetById(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public Either<Error, User> Create(UserCreate newUser)
        {
            throw new NotImplementedException();
        }

        public User CreateOld(UserCreate newUser)
        {
            var createdUser = new User
            {
                Id = Users.Count + 1,
                Name = newUser.Name,
                Email = newUser.Email
            };

            Users.Add(createdUser);

            return createdUser;
        }

        public User Update(UserUpdate updateUser)
        {
            if (updateUser == null)
            {
                return null;
            }

            // First? or default?
            var userToUpdate = Users.FirstOrDefault(u => u.Id == updateUser.Id);

            if (userToUpdate == null)
            {
                return null;
            }

            userToUpdate.Name = updateUser.Name;
            userToUpdate.Email = updateUser.Email;

            return userToUpdate;
        }

        public bool DeleteOld(int userId)
        {
            foreach (var user in Users)
            {
                if (user.Id == userId)
                {
                    Users.Remove(user);
                    return true;
                }
            }

            return false;
        }

        public Either<Error, Unit>Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Option<bool> DeleteWithOption(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
