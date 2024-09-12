namespace RepairServicesProviderBot.Core.InputModels
{
    public class ReviewInputModel
    {
        public int OrderId { get; set; }

        public string? ReviewDescription { get; set; }

        public int Rating { get; set; }
    }
}
