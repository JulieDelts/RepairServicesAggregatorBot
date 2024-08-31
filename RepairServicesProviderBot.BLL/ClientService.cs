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
        public UserRepository ClientRepository { get; set; }

        private Mapper _mapper;

        public ClientService()
        {
            ClientRepository = new UserRepository();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ClientMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public int AddClient(UserInputModel client)
        {
            var clientDTO = _mapper.Map<UserDTO>(client);

            var clientId = ClientRepository.AddUser(clientDTO);

            return clientId;
        }

        public ClientOutputModel GetClientById(long clientId)
        {
            var clientDTO = ClientRepository.GetUserById(clientId);
                
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

        public void HideClientById(int clientId)
        {
            ClientRepository.HideUserById(clientId);
        }

    }
}
