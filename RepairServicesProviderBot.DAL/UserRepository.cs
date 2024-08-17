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
                string query = Querries.UserQuerys.AddUserQuery;

                var args = new { id = user.Id, 
                    name = user.Name,
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
    }
}
