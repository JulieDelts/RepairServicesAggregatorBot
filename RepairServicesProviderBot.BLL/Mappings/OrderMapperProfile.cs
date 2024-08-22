using AutoMapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesProviderBot.BLL.Mappings
{
    public class OrderMapperProfile:Profile
    {
        public OrderMapperProfile() 
        {
            CreateMap<OrderInputModel, OrderDTO>();
            CreateMap<OrderDTO, CompletedOrderOutputModel>();
            CreateMap<OrderDTO, ConfirmedOrderOutputModel>();
            CreateMap<OrderDTO, UnConfirmedOrderOutputModel>();
        }
    }
}
