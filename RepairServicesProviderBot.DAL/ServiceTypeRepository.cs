using Npgsql;
using RepairServicesProviderBot.Core;
using RepairServicesProviderBot.Core.DTOs;
using Dapper;
using RepairServicesProviderBot.DAL.Querries;

namespace RepairServicesProviderBot.DAL
{
    public class ServiceTypeRepository
    {
        public int AddServiceType(string description)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.AddServiceTypeQuery;

                var args = new
                {
                    description = description
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int AddContractorServiceType(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.AddContractorServiceTypeQuery;

                var args = new
                {
                    userId = serviceType.UserId,
                    serviceTypeId = serviceType.Id,
                    cost = serviceType.Cost
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public ServiceTypeDTO GetServiceTypeById(int serviceTypeId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.GetServiceTypeByIdQuery;

                var args = new
                {
                    serviceTypeId = serviceTypeId
                };

                connection.Open();

                return connection.QuerySingle<ServiceTypeDTO>(query,args);
            }
        }

        public ServiceTypeDTO GetContractorServiceType(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.GetContractorServiceTypeByIdQuery;

                var args = new
                {
                    userId = serviceType.UserId,
                    serviceTypeId = serviceType.Id
                };

                connection.Open();

                return connection.QuerySingle<ServiceTypeDTO>(query, args);
            }
        }

        public List<ServiceTypeDTO> GetAvailableServiceTypes()
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.GetAvailableServiceTypesQuery;

                connection.Open();

                return connection.Query<ServiceTypeDTO>(query).ToList();
            }
        }

        public List<ServiceTypeDTO> GetContractorServiceTypesById(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.GetContractorServiceTypesByIdQuery;

                var args = new
                {
                    contractorId = userId
                };

                connection.Open();

                return connection.Query<ServiceTypeDTO>(query, args).ToList();
            }
        }

        public int UpdateServiceType(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.UpdateServiceTypeByIdQuery;

                var args = new
                {
                    serviceTypeId = serviceType.Id,
                    description = serviceType.ServiceTypeDescription,
                    isDeleted = serviceType.IsDeleted
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int UpdateContractorServiceCost(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.UpdateContractorServiceCostByIdQuery;

                var args = new
                {
                    userId = serviceType.UserId,
                    serviceTypeId = serviceType.Id,
                    cost = serviceType.Cost
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public void HideServiceTypeById(int ServiceTypeId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.HideServiceTypeByIdQuery;

                var args = new
                {
                    serviceTypeId = ServiceTypeId
                };

                connection.Open();

                connection.Execute(query, args);
            }
        }

        public void DeleteContractorServiceType(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.DeleteContractorServiceTypeByIdQuery;

                var args = new
                {
                    serviceTypeId = serviceType.Id,
                    userId = serviceType.UserId
                };

                connection.Open();

                connection.Execute(query, args);
            }
        }
    }
}
