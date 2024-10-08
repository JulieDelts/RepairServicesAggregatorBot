﻿namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ExtendedContractorOutputModel
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string? Image { get; set; }

        public double? Rating { get; set; }

        public bool IsDeleted { get; set; }
    }
}
