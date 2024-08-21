using RepairServicesProviderBot.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class CompletedOrderOutputModel
    {
        //public ClientOutputModel Client { get; set; }

        //public ContractorOutputModel Contractor { get; set; }

        //public ServiceOutputModel Service { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        //public ReviewOutputModel? Review { get; set; }
    }
}
