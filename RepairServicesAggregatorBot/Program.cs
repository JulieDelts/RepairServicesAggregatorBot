using RepairServicesProviderBot.DAL;
using RepairServicesProviderBot.Core.DTOs;


namespace RepairServicesAggregatorBot

{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            var u = new UserDTO();
            u.Id = 1000000;
            userRepository.AddUser(u);
        }
    }
}
