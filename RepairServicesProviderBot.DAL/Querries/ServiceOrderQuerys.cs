﻿namespace RepairServicesProviderBot.DAL.Querries
{
    public class ServiceOrderQuerys
    {
        public const string AddServiceOrder = $"SELECT * FROM \"AddServiceOrder\"(@clientId, @serviceTypeId,  @contractorId, @adminId, @statusId, @date, @description, @address, @isDeleted)";

        public const string GetServiceOrderById = $"SELECT * FROM \"GetServiceOrderById\"(@orderId)";

    }
}
