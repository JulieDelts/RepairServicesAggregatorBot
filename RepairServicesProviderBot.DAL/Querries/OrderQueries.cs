﻿namespace RepairServicesProviderBot.DAL.Querries
{
    public class OrderQueries
    {
        public const string AddOrderQuery = $"SELECT * FROM \"AddOrder\"(@clientId, @serviceTypeId, @contractorId, @adminId, @statusId, @orderDate, @description, @address);";

        public const string AddContractorReadyToAcceptOrderQuery = $"SELECT * FROM \"AddContractorReadyToAcceptOrder\"(@userId, @orderId)";

        public const string GetOrderByIdQuery = $"SELECT * FROM \"GetOrderById\"(@orderId)";

        public const string GetContractorsReadyToAcceptOrderByIdQuery = $"SELECT * FROM \"GetContractorsReadyToAcceptOrderById\"(@orderId)";

        public const string UpdateOrderByIdQuery = $"SELECT * FROM \"UpdateOrderById\"(@orderId, @clientId, @serviceTypeId,@contractorId, @adminId, @statusId, @orderDate, @description, @address, @isDeleted)";

        public const string HideOrderByIdQuery = $"SELECT * FROM \"HideOrderById\"(@orderId)";
    }
}
