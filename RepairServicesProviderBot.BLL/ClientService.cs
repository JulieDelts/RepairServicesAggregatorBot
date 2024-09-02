using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class ClientService
    {
        public UserRepository UserRepository { get; set; }

        private Mapper _mapper;

        public ClientService()
        {
            UserRepository = new UserRepository();

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

        //public ClientWithOrdersOutputModel GetClientWithOrdersById(int clientId)
        //{
        //    var clientDTO = ClientRepository.GetUserById(clientId);

        //    var client = _mapper.Map<ClientWithOrdersOutputModel>(clientDTO);
        //    var orders = 
        //    client.Orders

        //    return client;
        //}
    }
}
