namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ContractorWithServiceTypesOutputModel: ExtendedContractorOutputModel
    {
        public List<ContractorServiceTypeOutputModel> ServiceTypes { get; set; }
    }
}
