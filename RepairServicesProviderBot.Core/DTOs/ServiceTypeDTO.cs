using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.DTOs
{
    public class ServiceTypeDTO
    {
        public int? Id { get; set; }

        public string? Description { get; set; }

        public int? Cost { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
