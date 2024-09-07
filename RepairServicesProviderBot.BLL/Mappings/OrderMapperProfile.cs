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
            CreateMap<OrderDTO, InitialOrderOutputModel>();
            CreateMap<OrderDTO, ConfirmedOrderOutputModel>();
            CreateMap<OrderDTO, UnassignedOrderOutputModel>();
            CreateMap<OrderDTO, AssignedOrderOutputModel>();
            CreateMap<OrderDTO, CompletedOrderOutputModel>();
            CreateMap<OrderDTO, CancelledOrderOutputModel>();
        }
    }
}
