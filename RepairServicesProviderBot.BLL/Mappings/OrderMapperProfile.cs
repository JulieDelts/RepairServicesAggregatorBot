using AutoMapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesProviderBot.BLL.Mappings
{
    public class OrderMapperProfile:Profile
    {
        public OrderMapperProfile() 
        {
            
            CreateMap<CompletedOrderOutputModel, OrderDTO>();
            CreateMap<ConfirmedOrderOutputModel, OrderDTO>();
            CreateMap<UnConfirmedOrderOutputModel, OrderDTO>();

            CreateMap<OrderDTO, CompletedOrderOutputModel>();
            CreateMap<OrderDTO, ConfirmedOrderOutputModel>();
            CreateMap<OrderDTO, UnConfirmedOrderOutputModel>();
        }
    }
}
