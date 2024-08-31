using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ClientOutputModel
    {
        public string Name { get; set; }

        public long ChatId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Image { get; set; }
    }
}
