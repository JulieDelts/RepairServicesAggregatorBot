using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        public string? ClientName { get; set; }

        public int? ContractorId { get; set; }

        public string? ContractorName { get; set; }

        public int? AdminId { get; set; }

        public string? AdminName { get; set; }

        public int? ServiceTypeId { get; set; }

        public int? StatusId { get; set; }

        public string? StatusDescription { get; set; }

        public int Cost { get; set; }

        public string? Date { get; set; }

        public string? OrderDescription { get; set; }

        public string? Address { get; set; }

        public bool? IsDeleted { get; set; }

        public ServiceTypeDTO? ServiceType { get; set; }

    }
}
