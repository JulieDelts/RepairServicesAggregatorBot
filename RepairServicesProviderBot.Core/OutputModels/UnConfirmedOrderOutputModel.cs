using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class UnConfirmedOrderOutputModel
    {
        //public ClientOutputModel Client { get; set; }

        //publicList<ContractorOutputModel>? AvailableContractors { get; set; }

        //public ServiceOutputModel Service { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
