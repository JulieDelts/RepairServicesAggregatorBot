using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.DTOs;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ContractorWithServiceTypesOutputModel: ExtendedContractorOutputModel
    {
        public List<ContractorServiceTypeOutputModel> ServiceTypes { get; set; }
    }
}
