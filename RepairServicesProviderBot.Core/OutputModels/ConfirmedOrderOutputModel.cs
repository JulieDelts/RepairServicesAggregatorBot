﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ConfirmedOrderOutputModel: InitialOrderOutputModel
    {
        public ExtendedServiceTypeOutputModel ServiceType { get; set; }
    }
}
