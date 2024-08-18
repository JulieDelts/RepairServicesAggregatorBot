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
                string query = UserQuerys.AddUserQuery;

                var args = new
                {
                    userId = user.Id,
                    userName = user.Name,
                    email = user.Email,
                    phone = user.Phone,
                    roleId = user.RoleId,
                    image = user.Image,
                    isDeleted = user.IsDeleted
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int SetUserRoleById(UserDTO user)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQuerys.SetUserRoleByIdQuery;

                var args = new
                {
                    userId = user.Id,
                    roleId = user.RoleId
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int HideUserById(UserDTO user)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQuerys.HideUserById;

                var args = new
                {
                    userId = user.Id
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public UserDTO GetUserById(int id)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQuerys.GetUserById;

                var args = new
                {
                    userId = id
                };

                connection.Open();

                return connection.QuerySingle<UserDTO>(query, args);
            }
        }

        public int AddContractorInfo(int userId, int serviceTypeId, int cost)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQuerys.AddContractorInfo;

                var args = new
                {
                    userId = userId,
                    serviceTypeId = serviceTypeId,
                    cost = cost
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }
    }
}
