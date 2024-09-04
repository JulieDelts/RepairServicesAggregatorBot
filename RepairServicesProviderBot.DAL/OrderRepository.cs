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
                    description = order.OrderDescription,
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

        public List<UserDTO> GetContractorsReadyToAcceptOrderByOrderId(int orderId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetContractorsReadyToAcceptOrderByOrderIdQuery;

                var args = new
                {
                    orderId = orderId
                };

                connection.Open();

                return connection.Query<UserDTO, ServiceTypeDTO, UserDTO>(query,
                    (userDTO, serviceTypeDTO) =>
                    { userDTO.ServiceType = serviceTypeDTO;
                        return userDTO; },
                    args,
                    splitOn: "Cost").ToList();
            }
        }

        public List<OrderDTO> GetAllContractorOrdersByContractorId(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetAllContractorOrdersByContractorIdQuery;

                var args = new
                {
                    userId = userId
                };

                connection.Open();

                return connection.Query<OrderDTO, ServiceTypeDTO, OrderDTO>(query,
                    (orderDTO, serviceTypeDTO) =>
                    {
                        orderDTO.ServiceType = serviceTypeDTO;
                        return orderDTO;
                    },
                    args,
                    splitOn: "ServiceTypeDescription").ToList();

            }
        }

        public List<OrderDTO> GetCurrentContractorOrdersByContractorId(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetCurrentContractorOrdersByContractorIdQuery;

                var args = new
                {
                    userId = userId
                };

                connection.Open();

                return connection.Query<UserDTO, OrderDTO, ServiceTypeDTO, OrderDTO>(query,
                    (userDTO, orderDTO, serviceTypeDTO) =>
                    {
                        orderDTO.Client = userDTO;
                        orderDTO.ServiceType = serviceTypeDTO;
                        return orderDTO;
                    },
                    args,
                    splitOn: "OrderDescription, ServiceTypeDescription").ToList();

            }
        }

        public OrderDTO GetOrderForContractorConfirmation(int orderId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetOrderForContractorConfirmationQuery;

                var args = new
                {
                    orderId = orderId
                };

                connection.Open();

                return connection.Query<OrderDTO, ServiceTypeDTO, OrderDTO>(query,
                    (orderDTO, serviceTypeDTO) =>
                    {
                        orderDTO.ServiceType = serviceTypeDTO;
                        return orderDTO;
                    },
                    args,
                    splitOn: "ServiceTypeDescription").First();
            }
        }

        public List<OrderDTO> GetAllClientOrdersById(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetAllClientOrdersByIdQuery;

                var args = new
                {
                    userId = userId
                };

                connection.Open();

                return connection.Query<UserDTO, OrderDTO, ServiceTypeDTO, OrderDTO>(query,
                  (userDTO, orderDTO, serviceTypeDTO) =>
                  {
                      orderDTO.Contractor = userDTO;
                      orderDTO.ServiceType = serviceTypeDTO;
                      return orderDTO;
                  },
                  args,
                  splitOn: "OrderDescription, ServiceTypeDescription").ToList();
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
                    description = order.OrderDescription,
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
