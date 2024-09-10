using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.InputModels
{
    public class ExtendedOrderInputModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int? ContractorId { get; set; }

        public int? AdminId { get; set; }

        public int StatusId { get; set; }

        public string? Date {  get; set; }

        public string? OrderDescription { get; set; }

        public string? Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}
