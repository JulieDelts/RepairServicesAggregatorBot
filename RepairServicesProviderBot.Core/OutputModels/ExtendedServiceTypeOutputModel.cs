namespace RepairServicesProviderBot.Core.OutputModels
{
    public class ExtendedServiceTypeOutputModel
    {
        public int Id { get; set; }
        
        public string ServiceTypeDescription { get; set; }

        public bool IsDeleted { get; set; }
    }
}
