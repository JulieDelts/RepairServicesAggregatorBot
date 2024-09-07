namespace RepairServicesProviderBot.Core.OutputModels
{
    public class CompletedOrderOutputModel : InitialOrderOutputModel
    {
        public ReviewOutputModel? Review { get; set; }
    }
}
