using Npgsql;
using Dapper;
using RepairServicesProviderBot.Core;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.DAL.Querries;

namespace RepairServicesProviderBot.DAL
{
    public class UserRepository
    {
        public int AddUser(UserDTO user)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.AddUserQuery;

                var args = new
                {
                    userName = user.Name,
                    email = user.Email,
                    phone = user.Phone,
                    roleId = user.RoleId,
                    image = user.Image,
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public UserDTO GetUserById(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetUserByIdQuery;

                var args = new
                {
                    userId = userId
                };

                connection.Open();

                return connection.QuerySingle<UserDTO>(query, args);
            }
        }

        public int UpdateUserRoleById(int userId, int roleId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.UpdateUserRoleByIdQuery;

                var args = new
                {
                    userId = userId,
                    roleId = roleId
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int UpdateUser(UserDTO user)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.UpdateUserQuery;

                var args = new
                {
                    userName = user.Name,
                    phone = user.Phone,
                    email = user.Email,
                    roleId = user.RoleId,
                    image = user.Image,
                    isDeleted = user.IsDeleted,
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int HideUserById(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.HideUserByIdQuery;

                var args = new
                {
                    userId = userId
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }
    }
}
