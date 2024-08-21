using Dapper;
using Npgsql;
using RepairServicesProviderBot.Core;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.DAL.Querries;

namespace RepairServicesProviderBot.DAL
{
    public class OrderRepository
    {
        public int AddOrder(OrderDTO order)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.AddOrderQuery;

                var args = new
                {
                    clientId = order.ClientId,
                    serviceTypeId = order.ServiceTypeId,
                    contractorId = order.ContractorId,
                    adminId = order.AdminId,
                    statusId = order.StatusId,
                    orderDate = order.Date,
                    description = order.Description,
                    address = order.Address
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int AddContractorReadyToAcceptOrder(int userId, int orderId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.AddContractorReadyToAcceptOrderQuery;

                var args = new
                {
                    userId = userId,
                    orderId = orderId
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public OrderDTO GetOrderById(int orderId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetOrderByIdQuery;

                var args = new
                {
                    orderId = orderId
                };

                connection.Open();

                return connection.QuerySingle<OrderDTO>(query, args);
            }
        }

        public int UpdateOrderById(OrderDTO order)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.UpdateOrderByIdQuery;

                var args = new
                {
                    orderId = order.Id,
                    clientId = order.ClientId,
                    serviceTypeId = order.ServiceTypeId,
                    contractorId = order.ContractorId,
                    adminId = order.AdminId,
                    statusId = order.StatusId,
                    orderDate = order.Date,
                    description = order.Description,
                    address = order.Address,
                    isDeleted = order.IsDeleted
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public OrderDTO HideOrderById(int orderId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.HideOrderByIdQuery;

                var args = new
                {
                    orderId = orderId
                };

                connection.Open();

                return connection.QuerySingle<OrderDTO>(query, args);
            }
        }
    }
}
