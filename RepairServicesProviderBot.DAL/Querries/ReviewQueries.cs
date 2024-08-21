namespace RepairServicesProviderBot.DAL.Querries
{
    public class ReviewQueries
    {
        public const string AddReviewQuery = $"SELECT * FROM \"AddReview\"(@orderId, @description, @image, @rating)";
    }
}
