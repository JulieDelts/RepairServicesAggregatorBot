namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ExtendedOrderOutputModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int? ContractorId { get; set; }

        public int? AdminId { get; set; }

        public int StatusId { get; set; }

        public string Date { get; set; }

        public string OrderDescription { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}
