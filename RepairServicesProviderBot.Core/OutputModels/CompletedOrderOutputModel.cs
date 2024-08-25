using RepairServicesProviderBot.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class CompletedOrderOutputModel: OrderOutputModel
    {
        public ContractorWithServiceTypeOutputModel Contractor { get; set; }

        //public ReviewOutputModel? Review { get; set; }
    }
}
