using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ConfirmedOrderOutputModel: InitialOrderOutputModel
    {
        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public ExtendedServiceTypeOutputModel ServiceType { get; set; }
    }
}
