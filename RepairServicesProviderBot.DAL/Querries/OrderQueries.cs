namespace RepairServicesProviderBot.DAL.Querries
{
    public class OrderQueries
    {
        public const string AddOrderQuery = $"SELECT * FROM \"AddOrder\"(@clientId, @serviceTypeId, @contractorId, @adminId, @statusId, @orderDate, @description, @address);";

        public const string GetOrderByIdQuery = $"SELECT * FROM \"GetOrderById\"(@orderId)";

        public const string HideOrderByIdQuery = $"SELECT * FROM \"HideOrderById\"(@orderId)";
    }
}
