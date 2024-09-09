using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ContractorServiceTypeOutputModel
    {
        public int Id { get; set; }

        public string ServiceTypeDescription { get; set; }

        public int Cost { get; set; }
    }
}
