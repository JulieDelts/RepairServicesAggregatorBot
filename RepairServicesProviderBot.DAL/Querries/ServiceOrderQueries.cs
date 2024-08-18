namespace RepairServicesProviderBot.DAL.Querries
{
    public class ServiceOrderQueries
    {
        public const string AddServiceOrder = $"SELECT * FROM \"AddServiceOrder\"(@clientId, @serviceTypeId,  @contractorId, @adminId, @statusId, @orderDate, @description, @address, @isDeleted)";

        public const string GetServiceOrderById = $"SELECT * FROM \"GetServiceOrderById\"(@orderId)";

    }
}
