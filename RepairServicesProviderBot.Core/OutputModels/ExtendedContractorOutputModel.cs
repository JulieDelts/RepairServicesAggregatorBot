using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ExtendedContractorOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        public double? Rating { get; set; }

        public string IsDeleted { get; set; }

    }
}
