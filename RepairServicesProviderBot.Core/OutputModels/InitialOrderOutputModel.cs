namespace RepairServicesProviderBot.Core.OutputModels
{
    public class InitialOrderOutputModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public int StatusId { get; set; }

        public string StatusDescription { get; set; }

        public string Date { get; set; }

        public string OrderDescription { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}
