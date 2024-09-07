using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class CancelledOrderOutputModel: InitialOrderOutputModel
    {
        public int? ContractorId { get; set; }

        public int? Cost { get; set; }

        public ExtendedServiceTypeOutputModel? ServiceType { get; set; }

        public List<ContractorWithServiceTypeOutputModel>? AvailableContractors { get; set; }

        public ContractorWithServiceTypeOutputModel? Contractor { get; set; }

        public ReviewOutputModel? Review { get; set; }
    }
}
