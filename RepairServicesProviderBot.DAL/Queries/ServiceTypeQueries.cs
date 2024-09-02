using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.DAL.Querries
{
    public class ServiceTypeQueries
    {
        public const string AddServiceTypeQuery = "SELECT * FROM \"AddServiceType\"(@description);";

        public const string AddContractorServiceTypeQuery = $"SELECT * FROM \"AddContractorServiceType\"(@userId, @serviceTypeId, @cost)";

        public const string GetServiceTypeByIdQuery = $"SELECT * FROM \"GetServiceTypeById\"(@serviceTypeId);";

        public const string GetContractorServiceTypeByIdQuery = $"SELECT * FROM \"GetContractorServiceTypeById\"(@userId, @serviceTypeId);";

        public const string GetAvailableServiceTypesQuery = "SELECT * FROM \"GetAvailableServiceTypes\"();";

        public const string GetContractorServiceTypesByIdQuery = $"SELECT * FROM \"GetContractorServiceTypesById\"(@contractorId)";

        public const string UpdateServiceTypeByIdQuery = "SELECT * FROM \"UpdateServiceTypeById\"(@serviceTypeId, @description, @isDeleted)";

        public const string UpdateContractorServiceCostByIdQuery = $"SELECT * FROM \"UpdateContractorServiceCostById\"(@userId, @serviceTypeId, @cost)";

        public const string HideServiceTypeByIdQuery = "SELECT * FROM \"HideServiceTypeById\"(@serviceTypeId);";

        public const string DeleteContractorServiceTypeByIdQuery = $"SELECT * FROM \"DeleteContractorServiceTypeById\"(@userId, @serviceTypeId);";
    }
}
