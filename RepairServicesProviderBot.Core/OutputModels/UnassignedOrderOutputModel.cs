using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class UnassignedOrderOutputModel: ConfirmedOrderOutputModel
    {
        public List<ContractorWithServiceTypeOutputModel> AvailableContractors { get; set; }
    }
}
