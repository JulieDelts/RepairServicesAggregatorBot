using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.DTOs;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ContractorWithOrdersAndServiceTypesOutputModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Image { get; set; }

        public string Rating { get; set; }

        public List<OrderOutputModel> Orders { get; set; }

        public List<ServiceTypeOutputModel> ServiceTypes { get; set; }

    }
}
