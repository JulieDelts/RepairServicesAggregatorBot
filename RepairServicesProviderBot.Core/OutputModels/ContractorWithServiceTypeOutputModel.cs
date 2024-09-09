using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.DTOs;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ContractorWithServiceTypeOutputModel: ExtendedContractorOutputModel
    {
        public ContractorServiceTypeOutputModel ServiceType { get; set; }
    }
}
