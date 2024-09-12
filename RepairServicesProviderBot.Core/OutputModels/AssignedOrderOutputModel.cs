namespace RepairServicesProviderBot.Core.OutputModels
{
    public class AssignedOrderOutputModel: ConfirmedOrderOutputModel
    {
        public int ContractorId { get; set; }

        public string ContractorName { get; set; }
        
        public int Cost { get; set; }
    }
}
