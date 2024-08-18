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

namespace RepairServicesProviderBot.DAL
{
    public class ServiceTypeRepository
    {
        public int AddServiceType(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.AddServiceType;

                var args = new
                {
                    description = serviceType.Description,
                    isDeleted = serviceType.IsDeleted
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public int HideServiceTypeById(int id)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.HideServiceTypeById;

                var args = new
                {
                    serviceTypeId = id
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public List<ServiceTypeDTO> GetAllServiceTypes()
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.GetAllServiceTypes;

                connection.Open();

                return connection.Query<ServiceTypeDTO>(query).ToList();
            }
        }

        public int UpdateServiceTypeById(ServiceTypeDTO serviceType)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.UpdateServiceTypeById;

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

        public List<ServiceTypeDTO> GetContractorServicesById(int contractorId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = Querries.ServiceTypeQueries.GetContractorServicesById;

                var args = new
                {
                    contractorId = contractorId,
                };

                connection.Open();

                return connection.Query<ServiceTypeDTO>(query, args).ToList();
            }
        }
    }
}
