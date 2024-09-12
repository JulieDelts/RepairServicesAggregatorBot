using AutoMapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesProviderBot.BLL.Mappings
{
    public class ContractorMapperProfile:Profile
    {
        public ContractorMapperProfile()
        {
            CreateMap<UserInputModel, UserDTO>();
            CreateMap<UserDTO, ContractorWithServiceTypesOutputModel>();
            CreateMap<UserDTO, ContractorWithServiceTypeOutputModel>();
            CreateMap<UserDTO, ExtendedContractorOutputModel>();
        }
    }
}
