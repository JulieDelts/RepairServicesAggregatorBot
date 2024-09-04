using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.DTOs
{
    public class ReviewDTO
    {
        public int? OrderId { get; set; }

        public string? ReviewDescription { get; set; }

        public int? Rating { get; set; }
    }
}
