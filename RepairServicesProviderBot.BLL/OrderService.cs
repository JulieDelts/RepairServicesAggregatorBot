using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairServicesProviderBot.BLL
{
    public class OrderService
    {
        public OrderRepository OrderRepository { get; set; }

        private Mapper _mapper;

        public OrderService()
        {
            OrderRepository = new OrderRepository();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new OrderMapperProfile());
                });
            _mapper = new Mapper(config);
        }
        public UnConfirmedOrderOutputModel GetUnConfirmedOrderById(int orderId)
        {
            var orderDTO = OrderRepository.GetOrderById(orderId);

            UnConfirmedOrderOutputModel orderResponce = new UnConfirmedOrderOutputModel();

            if (orderDTO != null)
            {
                orderResponce = _mapper.Map<UnConfirmedOrderOutputModel>(orderDTO);
            }

            return orderResponce;
        }

        public UnConfirmedOrderOutputModel AddOrder(OrderInputModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            var orderId = OrderRepository.AddOrder(orderDTO);

            var orderResponce = GetUnConfirmedOrderById(orderId);

            return orderResponce;
        }

    }
}
