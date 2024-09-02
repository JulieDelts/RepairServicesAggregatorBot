using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.InputModels
{
    public class ContractorServiceTypeInputModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Cost { get; set; }
    }
}
