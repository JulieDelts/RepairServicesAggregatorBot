using AutoMapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
namespace RepairServicesProviderBot.BLL.Mappings
{
    public class ClientMapperProfile:Profile
    {
        public ClientMapperProfile()
        {
            CreateMap<UserInputModel, UserDTO>();
            CreateMap<UserDTO, ClientOutputModel>();
            CreateMap<UserDTO, ClientWithOrdersOutputModel>();
        }
    }
}
