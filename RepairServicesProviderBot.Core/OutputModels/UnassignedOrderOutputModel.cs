namespace RepairServicesProviderBot.Core.OutputModels
{
    public class UnassignedOrderOutputModel: ConfirmedOrderOutputModel
    {
        public List<ContractorWithServiceTypeOutputModel> AvailableContractors { get; set; }
    }
}
