using Dapper;
using Npgsql;
using RepairServicesProviderBot.Core;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.DAL.Querries;

namespace RepairServicesProviderBot.DAL
{
    public class ServiceOrderRepository
    {
        public int AddServiceOrder(ServiceOrderDTO order)
        {
            string conectionString = Options.ConnectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceOrderQueries.AddServiceOrder;

                var args = new
                {
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

        public ServiceOrderDTO GetServiceOrderById(ServiceOrderDTO order)
        {
            string conectionString = Options.ConnectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceOrderQueries.GetServiceOrderById;

                var args = new
                {
                    orderId = order.Id
                };

                connection.Open();
                return connection.QuerySingle<ServiceOrderDTO>(query, args);
            }
        }
    }
}
