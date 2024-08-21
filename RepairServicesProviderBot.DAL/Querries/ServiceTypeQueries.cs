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

        public const string AddContractorServiceQuery = $"SELECT * FROM \"AddContractorService\"(@userId, @serviceTypeId, @cost)";

        public const string GetAvailableServiceTypesQuery = "SELECT * FROM \"GetAvailableServiceTypes\"();";

        public const string GetContractorServicesByIdQuery = $"SELECT * FROM \"GetContractorServicesById\"(@contractorId)";

        public const string UpdateServiceTypeByIdQuery = "SELECT * FROM \"UpdateServiceTypeById\"(@serviceTypeId, @description, @isDeleted)";

        public const string HideServiceTypeByIdQuery = "SELECT * FROM \"HideServiceTypeById\"(@serviceTypeId);";
    }
}
