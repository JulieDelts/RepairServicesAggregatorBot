using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.DAL.Querries
{
    public class UserQueries
    {
        public const string AddUser = $"SELECT * FROM \"AddUser\"(@userId, @userName, @phone, @email, @roleId, @image, @isDeleted)";

        public const string SetUserRoleById = $"SELECT * FROM \"SetUserRoleById\"(@userId, @roleId)";

        public const string HideUserById = $"SELECT * FROM \"HideUserById\"(@userId)";

        public const string GetUserById = $"SELECT * FROM \"GetUserById\"(@userId)";

        public const string AddContractorInfo = $"SELECT * FROM \"AddContractorInfo\"(@userId, @serviceTypeId, @cost)";

    }
}