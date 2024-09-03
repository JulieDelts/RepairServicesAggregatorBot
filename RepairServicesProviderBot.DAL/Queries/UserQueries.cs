using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.DAL.Querries
{
    public class UserQueries
    {
        public const string AddUserQuery = $"SELECT * FROM \"AddUser\"(@chatId, @userName, @phone, @email, @roleId, @image)";

        public const string GetUserByIdQuery = $"SELECT * FROM \"GetUserById\"(@userId)";

        public const string UpdateUserByIdQuery = $"SELECT * FROM \"UpdateUserById\"(@userId, @userName, @phone, @email, @roleId, @image, @isDeleted);";

        public const string HideUserByIdQuery = $"SELECT * FROM \"HideUserById\"(@userId)"; 

    }
}