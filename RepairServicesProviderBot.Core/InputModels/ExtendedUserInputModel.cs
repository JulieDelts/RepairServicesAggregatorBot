﻿namespace RepairServicesProviderBot.Core.InputModels
{
    public class ExtendedUserInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long ChatId { get; set; }

        public string? Email { get; set; }

        public string Phone { get; set; }

        public int RoleId { get; set; }

        public string? Image { get; set; }

        public bool IsDeleted { get; set; }
    }
}

