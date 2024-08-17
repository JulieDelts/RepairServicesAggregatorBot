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
            u.Id = 10700;
            int qwe =  userRepository.AddUser(u);
            Console.WriteLine(qwe);
        }
    }
}
