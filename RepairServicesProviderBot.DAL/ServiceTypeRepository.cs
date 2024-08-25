using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string query = Querries.ServiceTypeQueries.AddServiceTypeQuery;

                var args = new
                {
                    description = description
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int AddContractorService(int userId, int serviceTypeId, int cost)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.AddContractorServiceQuery;

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

        public List<ServiceTypeDTO> GetAvailableServiceTypes()
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.GetAvailableServiceTypesQuery;

                connection.Open();

                return connection.Query<ServiceTypeDTO>(query).ToList();
            }
        }

        public List<ServiceTypeDTO> GetContractorServicesById(int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.GetContractorServicesByIdQuery;

                var args = new
                {
                    contractorId = userId
                };

                connection.Open();

                return connection.Query<ServiceTypeDTO>(query, args).ToList();
            }
        }

        public int UpdateServiceTypeById(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.UpdateServiceTypeByIdQuery;

                var args = new
                {
                    serviceTypeId = serviceType.Id,
                    description = serviceType.Description,
                    isDeleted = serviceType.IsDeleted
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int UpdateContractorServiceCost(int userId, int serviceTypeId, int cost)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.UpdateContractorServiceCostQuery;

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

        public int HideServiceTypeById(int ServiceTypeId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.HideServiceTypeByIdQuery;

                var args = new
                {
                    serviceTypeId = ServiceTypeId
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public void DeleteContractorService(int serviceTypeId, int userId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ServiceTypeQueries.DeleteContractorServiceQuery;

                var args = new
                {
                    serviceTypeId = serviceTypeId,
                    userId = userId
                };

                connection.Open();

                connection.QuerySingle<int>(query, args);
            }
        }
    }
}
