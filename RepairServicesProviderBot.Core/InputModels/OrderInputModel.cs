namespace RepairServicesProviderBot.Core.InputModels
{
    public class OrderInputModel
    {
        public int ClientId { get; set; }

        public string OrderDescription { get; set; }

        public string Address { get; set; }

        public int StatusId { get; set; }

        public string Date { get; set; }

        public string Photo {  get; set; }
    }
}
