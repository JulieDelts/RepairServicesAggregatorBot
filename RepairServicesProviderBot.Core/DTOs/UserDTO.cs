using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.DTOs
{
    public class UserDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int? RoleId { get; set; }

        public string? RoleDescription { get; set; }

        public string? Image { get; set; }

        public bool? IsDeleted { get; set; }

        public List<OrderDTO>? Orders { get; set; }

        public List<ServiceTypeDTO>? Services { get; set; }

    }
}
