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
                    chatId = user.ChatId,
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

        public UserDTO GetUserByChatId(long chatId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetUserByChatIdQuery;

                var args = new
                {
                    chatId = chatId
                };

                connection.Open();

                return connection.QuerySingle<UserDTO>(query, args);
            }
        }

        public double? GetContractorRating(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetContractorRatingQuery;

                var args = new
                {
                    userId = userId
                };

                connection.Open();

                return connection.QuerySingle<double?>(query, args);
            }
        }

        public List<UserDTO> GetAllContractors()
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetAllContractorsQuery;

                connection.Open();

                return connection.Query<UserDTO>(query).ToList();
            }
        }

        public List<UserDTO> GetContractorsByServiceTypeId(int serviceTypeId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetContractorsByServiceTypeIdQuery;

                var args = new
                {
                    serviceTypeId = serviceTypeId
                };

                connection.Open();

                return connection.Query<UserDTO, ServiceTypeDTO, UserDTO>(query,
                    (userDTO, serviceTypeDTO) =>
                    {
                        userDTO.ServiceType = serviceTypeDTO;
                        return userDTO;
                    },
                    args,
                    splitOn:"Cost").ToList();
            }
        }

        public List<UserDTO> GetAllAdmins()
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetAllAdminsQuery;

                connection.Open();

                return connection.Query<UserDTO>(query).ToList();
            }
        }

        public int UpdateUserById(UserDTO user)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.UpdateUserByIdQuery;

                var args = new
                {
                    userId = user.Id,
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
