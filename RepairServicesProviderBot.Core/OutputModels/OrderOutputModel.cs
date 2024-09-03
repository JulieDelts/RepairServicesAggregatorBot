using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class OrderOutputModel
    {
        public ClientOutputModel Client { get; set; }

        public ServiceTypeOutputModel ServiceType { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
