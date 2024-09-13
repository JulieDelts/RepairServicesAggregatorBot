namespace RepairServicesProviderBot.Core
{
    public class Options
    {
        public static string ConnectionString
        {
            get 
            {
                return Environment.GetEnvironmentVariable("RepairsDB");
            }
        }

        public static string AdminPassword
        {
            get 
            {
                return Environment.GetEnvironmentVariable("AdminRepairsDB");
            }
        }

        public static string ContractorPassword
        {
            get 
            {
                return Environment.GetEnvironmentVariable("ContractorRepairsDB");
            }
        }

        public static string TgBotToken
        {
            get 
            {
                return Environment.GetEnvironmentVariable("TgBotToken");
            }
        }
    }
}
