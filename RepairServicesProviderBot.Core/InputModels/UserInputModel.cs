﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.Core.InputModels
{
    public class UserInputModel
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int RoleId { get; set; }

        public string? Image { get; set; }
    }
}
