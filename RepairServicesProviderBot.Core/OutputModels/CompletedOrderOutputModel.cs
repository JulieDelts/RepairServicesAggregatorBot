namespace RepairServicesProviderBot.Core.OutputModels
{
    public class CompletedOrderOutputModel : AssignedOrderOutputModel
    {
        public ReviewOutputModel? Review { get; set; }
    }
}
