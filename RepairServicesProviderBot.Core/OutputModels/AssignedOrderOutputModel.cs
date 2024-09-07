using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class AssignedOrderOutputModel: ConfirmedOrderOutputModel
    {
        public int ContractorId { get; set; }

        public int Cost { get; set; }

        public ContractorWithServiceTypeOutputModel Contractor { get; set; }

    }
}
