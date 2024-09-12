namespace RepairServicesProviderBot.Core.DTOs
{
    public class ServiceTypeDTO
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string ServiceTypeDescription { get; set; }

        public int? Cost { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
