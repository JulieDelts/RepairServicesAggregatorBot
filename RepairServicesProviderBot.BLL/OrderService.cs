using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

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

            var orderResponce = _mapper.Map<UnConfirmedOrderOutputModel>(orderDTO);

            return orderResponce;
        }

<<<<<<< Updated upstream

=======
        public OrderOutputModel GetOrderById(int orderId)
        {
            OrderOutputModel orderResponse = new OrderOutputModel();

            var contractorDTOs = OrderRepository.GetContractorsReadyToAcceptOrderByOrderId(orderId);  
            
            var contractors = _mapper.Map<List<ContractorWithServiceTypeOutputModel>>(contractorDTOs);

            var orderDTO = OrderRepository.GetOrderById(orderId);

            if (contractors.Count>0 & orderDTO.StatusId==0)
            {
                orderResponse = new ConfirmedOrderOutputModel();
                orderResponse = _mapper.Map<ConfirmedOrderOutputModel>(orderDTO);
                orderResponse.Contractor = contractors[0];
            }
            else if (contractors.Count > 0)
            {
                orderResponse = _mapper.Map<UnConfirmedOrderOutputModel>(orderDTO);
                orderResponse.AvailableContractors = contractors;
            }
            else
            {

                orderResponse = _mapper.Map<CompletedOrderOutputModel>(orderDTO);
                orderResponse.Contractor = contractors[0];
                //ЗДЕСЬ ОТЗЫВ ДОБАВЛЯЕМ
            }

            return orderResponce;
        }
>>>>>>> Stashed changes

        public UnConfirmedOrderOutputModel AddOrder(OrderInputModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            var orderId = OrderRepository.AddOrder(orderDTO);

            var orderResponce = GetUnConfirmedOrderById(orderId);

            return orderResponce;
        }

    }
}
