using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class AssignedOrderOutputModel: UnassignedOrderOutputModel
    {
        public int ContractorId { get; set; }

        public string ContractorName { get; set; }
        
        public int Cost { get; set; }

    }
}
