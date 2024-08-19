using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.DTOs
{
    public class ReviewDTO
    {
        public int? Id { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public double? Rating { get; set; }
    }
}
