namespace RepairServicesProviderBot.Core.OutputModels
{
    public class AdminOutputModel
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public string Phone { get; set; }

        public string? Image { get; set; }
    }
}
