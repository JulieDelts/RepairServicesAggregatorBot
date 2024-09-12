using AutoMapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesProviderBot.BLL.Mappings
{
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<UserInputModel, UserDTO>();
            CreateMap<ExtendedUserInputModel, UserDTO>();
            CreateMap<UserDTO,ExtendedUserOutputModel>();
        }
    }
}
