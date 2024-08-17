using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.DAL.Querries
{
    public class ServiceTypeQueries
    {
        public const string AddServiceType = "SELECT * FROM \"AddServiceType\"(@description);";

        public const string HideServiceTypeById = "SELECT * FROM \"HideServiceTypeById\"(@serviceTypeId);";

        public const string GetAllServiceTypes = "SELECT * FROM \"GetAllServiceTypes\"(@serviceTypeId);";

        public const string UpdateServiceTypeById = "SELECT * FROM \"UpdateServiceTypeById\"(@serviceTypeId, @description, @isDeleted)";
        
        //slozno...
    }
}
