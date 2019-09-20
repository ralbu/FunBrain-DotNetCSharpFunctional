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

namespace FunBrainSqlPersistence
{
    public class UserRepository : IUserRepository
    {

        public Option<IList<User>> Get()
        {
            var users = new List<User>();

            string sql = "select * from Users";
            string cnString = "Data Source=.;Initial Catalog=FunBrain;Integrated Security=True;";
            using (var cn = new SqlConnection(cnString))
            {
                users = cn.Query<User>(sql).ToList();
            }

            return users;
        }
        
        public User GetById(int id)
        {
            return null;
        }

        public User Create(UserCreate newUser) { return null; }
        public User Update(UserUpdate updateUser) { return null; }
        public bool Delete(int userId) { return false; }
    }

}
