namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ClientWithOrdersOutputModel
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string Phone { get; set; }

        public string? Image { get; set; }

        public List<InitialOrderOutputModel>? Orders { get; set; }
    }
}
