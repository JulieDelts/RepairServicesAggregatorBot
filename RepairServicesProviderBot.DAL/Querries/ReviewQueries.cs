namespace RepairServicesProviderBot.DAL.Querries
{
    public class ReviewQueries
    {
        public const string AddReview = $"SELECT * FROM \"AddReview\"(@serviceOrderId, @description, @image, @rating)";
    }
}
