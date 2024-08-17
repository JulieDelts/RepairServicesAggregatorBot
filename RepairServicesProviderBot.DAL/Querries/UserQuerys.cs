using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//UliaHahahahahhahahahahha
namespace RepairServicesProviderBot.DAL.Querries
{
    public class UserQuerys
    {
        public const string AddUserQuery = $"SELECT * FROM \"AddUser\"(@userId, @userName, @phone, @email, @roleId, @image, @isDeleted)";

        public const string SetUserRoleByIdQuery = $"SELECT * FROM \"SetUserRoleById\"(@userId, @roleId)";

        public const string HideUserById = $"SELECT * FROM \"HideUserById\"(@userId)";

    }
}