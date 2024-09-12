using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class ClientService
    {
        public UserRepository UserRepository { get; set; }

        public OrderRepository OrderRepository { get; set; }

        private Mapper _mapper;

        public ClientService()
        {
            UserRepository = new();

            OrderRepository = new();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ClientMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public ClientOutputModel GetClientById(int clientId)
        {
            var clientDTO = UserRepository.GetUserById(clientId);
                
            var client = _mapper.Map<ClientOutputModel>(clientDTO);

            return client;
        }
    }
}
