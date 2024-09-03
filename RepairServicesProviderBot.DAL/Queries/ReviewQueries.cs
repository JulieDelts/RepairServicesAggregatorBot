namespace RepairServicesProviderBot.DAL.Querries
{
    public class ReviewQueries
    {
        public const string AddReviewByOrderIdQuery = $"SELECT * FROM \"AddReviewByOrderId\"(@orderId, @description, @rating);";

        public const string GetReviewByOrderIdQuery = $"SELECT * FROM \"GetReviewByOrderId\"(@orderId);";
    }
}
