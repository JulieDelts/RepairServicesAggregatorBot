namespace RepairServicesProviderBot.DAL.Querries
{
    public class UserQueries
    {
        public const string AddUserQuery = $"SELECT * FROM \"AddUser\"(@chatId, @userName, @phone, @email, @roleId, @image);";

        public const string GetUserByIdQuery = $"SELECT * FROM \"GetUserById\"(@userId);";

        public const string GetUserByChatIdQuery = $"SELECT * FROM \"GetUserByChatId\"(@chatId);";

        public const string GetContractorRatingQuery = $"SELECT * FROM \"GetContractorRating\"(@userId);";

        public const string GetAllContractorsQuery = $"SELECT * FROM \"GetAllContractors\"()";

        public const string GetContractorsByServiceTypeIdQuery = $"SELECT * FROM \"GetContractorsByServiceTypeId\"(@serviceTypeId);";

        public const string GetAllAdminsQuery = $"SELECT * FROM \"GetAllAdmins\"();";

        public const string UpdateUserByIdQuery = $"SELECT * FROM \"UpdateUserById\"(@userId, @userName, @phone, @email, @roleId, @image, @isDeleted);";

        public const string HideUserByIdQuery = $"SELECT * FROM \"HideUserById\"(@userId);"; 
    }
}