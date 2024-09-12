namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ConfirmedOrderOutputModel: InitialOrderOutputModel
    {
        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public ExtendedServiceTypeOutputModel ServiceType { get; set; }
    }
}
