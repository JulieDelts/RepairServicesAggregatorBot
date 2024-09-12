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
            OrderRepository = new();

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new OrderMapperProfile());
                    cfg.AddProfile(new ContractorMapperProfile());
                    cfg.AddProfile(new ClientMapperProfile());
                    cfg.AddProfile(new ServiceTypeMapperProfile());
                    cfg.AddProfile(new ReviewMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public int AddOrder(OrderInputModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            var orderId = OrderRepository.AddOrder(orderDTO);

            return orderId;
        }

        public int AddContractorReadyToAcceptOrder(int userId, int orderId)
        {
            int user = OrderRepository.AddContractorReadyToAcceptOrder(userId, orderId);

            return user;
        }

        public List<ContractorWithServiceTypeOutputModel> GetGetContractorsReadyToAcceptOrderByOrderId(int orderId)
        {
            var contractorDTOs = OrderRepository.GetContractorsReadyToAcceptOrderByOrderId(orderId);

            List<ContractorWithServiceTypeOutputModel> contractors = new();

            foreach (var contractorDTO in contractorDTOs)
            {
                var contractor = _mapper.Map<ContractorWithServiceTypeOutputModel>(contractorDTO);

                contractors.Add(contractor);
            }

            return contractors;
        }

        public InitialOrderOutputModel GetOrderById(int orderId)
        {
            var orderDTO = OrderRepository.GetOrderById(orderId);

            var order = MapOrderDTOToOutputModel(orderDTO);

            return order;
        }

        public List<InitialOrderOutputModel> GetAllOrdersByUserId(int userId)
        {
            var orderDTOs = OrderRepository.GetAllOrdersByUserId(userId);

            List<InitialOrderOutputModel> orders = new();

            ReviewService reviewService = new();

            foreach (var orderDTO in orderDTOs)
            {
                InitialOrderOutputModel order = MapOrderDTOToOutputModel(orderDTO);

                orders.Add(order);
            }

            return orders;
        }

        public List<InitialOrderOutputModel> GetNewOrders()
        {
            var orderDTOs = OrderRepository.GetNewOrders();

            List<InitialOrderOutputModel> orders = new();

            foreach (var orderDTO in orderDTOs)
            {
                InitialOrderOutputModel order = _mapper.Map<InitialOrderOutputModel>(orderDTO);

                orders.Add(order);
            }

            return orders;
        }

        public ExtendedOrderOutputModel GetOrderSystemInfoById(int orderId)
        {
            var orderDTO = OrderRepository.GetOrderSystemInfoById(orderId);

            var order = _mapper.Map<ExtendedOrderOutputModel>(orderDTO);

            return order;
        }

        public ConfirmedOrderOutputModel GetOrderForContractorConfirmation(int orderId)
        {
            var orderDTO = OrderRepository.GetOrderForContractorConfirmation(orderId);

            var order = _mapper.Map<ConfirmedOrderOutputModel>(orderDTO);

            return order;
        }

        public int UpdateOrder(ExtendedOrderInputModel extendedOrderInputModel)
        {
            var orderDTO = _mapper.Map<OrderDTO>(extendedOrderInputModel);

            int orderId = OrderRepository.UpdateOrder(orderDTO);

            return orderId;
        }

        public int HideOrderById(int orderId)
        {
            int order = OrderRepository.HideOrderById(orderId);

            return order;
        }

        private InitialOrderOutputModel MapOrderDTOToOutputModel(OrderDTO orderDTO)
        {
            ReviewService reviewService = new();

            InitialOrderOutputModel order = new();

            if (orderDTO.StatusId == 0)
            {
                order = _mapper.Map<InitialOrderOutputModel>(orderDTO);
            }
            else if (orderDTO.StatusId == 1)
            {
                order = _mapper.Map<ConfirmedOrderOutputModel>(orderDTO);
            }
            else if (orderDTO.StatusId == 2)
            {
                var unassignedOrder = _mapper.Map<UnassignedOrderOutputModel>(orderDTO);

                var contractors = GetGetContractorsReadyToAcceptOrderByOrderId((int)orderDTO.Id);

                unassignedOrder.AvailableContractors = contractors;

                order = unassignedOrder;
            }
            else if (orderDTO.StatusId == 3 || orderDTO.StatusId == 4)
            {
                order = _mapper.Map<AssignedOrderOutputModel>(orderDTO);
            }
            else if (orderDTO.StatusId == 5)
            {
                var completedOrder = _mapper.Map<CompletedOrderOutputModel>(orderDTO);

                var review = reviewService.GetReviewByOrderId((int)orderDTO.Id);

                completedOrder.Review = review;

                order = completedOrder;
            }
            else if (orderDTO.StatusId == 6)
            {
                var cancelledOrder = _mapper.Map<CancelledOrderOutputModel>(orderDTO);

                var contractors = GetGetContractorsReadyToAcceptOrderByOrderId((int)orderDTO.Id);

                cancelledOrder.AvailableContractors = contractors;

                var review = reviewService.GetReviewByOrderId((int)orderDTO.Id);

                cancelledOrder.Review = review;

                order = cancelledOrder;
            }

            return order;
        }
    }
}
