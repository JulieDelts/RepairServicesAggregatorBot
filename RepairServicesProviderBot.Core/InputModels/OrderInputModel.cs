using RepairServicesProviderBot.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.InputModels
{
    public class OrderInputModel
    {
        //public long ClientId { get; set; }

        public UserInputModel Client { get; set; }

        public string OrderDescription { get; set; }

        public string Address { get; set; }

        public int StatusId { get; set; }

        public string Date { get; set; }
    }
}
