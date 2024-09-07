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
                cfg =>
                {
                    cfg.AddProfile(new OrderMapperProfile());
                    cfg.AddProfile(new ContractorMapperProfile());
                    cfg.AddProfile(new ServiceTypeMapperProfile());
                    cfg.AddProfile(new ReviewMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        //public InitialOrderOutputModel AddOrder(OrderInputModel order)
        //{
        //    var orderDTO = _mapper.Map<OrderDTO>(order);

        //    var orderId = OrderRepository.AddOrder(orderDTO);

        //    //var orderResponce = GetOrderById(orderId);

        //    return orderResponce;
        //}

        public void AddContractorReadyToAcceptOrder(int userId, int orderId)
        {
            OrderRepository.AddContractorReadyToAcceptOrder(userId, orderId);
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

        //public List<InitialOrderOutputModel> GetAllContractorOrdersByContractorId(int userId)
        //{
        //    var orderDTOs = OrderRepository.GetAllContractorOrdersByContractorId(userId);

        //    List<InitialOrderOutputModel> orders = new List<InitialOrderOutputModel>();

        //    ReviewService reviewService = new ReviewService();

        //    foreach (var orderDTO in orderDTOs)
        //    {
        //        InitialOrderOutputModel order = MapOrderDTOToOutputModel(orderDTO);

        //        orders.Add(order);
        //    }

        //    return orders;
        //}

        //public InitialOrderOutputModel GetOrderById(int orderId)
        //{
        //    var orderDTO = OrderRepository.GetOrderById(orderId);

        //    var order = MapOrderDTOToOutputModel(orderDTO);

        //    return order;
        //}

        //public List<InitialOrderOutputModel> GetAllClientOrdersById(int userId)
        //{
        //    var orderDTOs = OrderRepository.GetAllClientOrdersById(userId);

        //    List<InitialOrderOutputModel> orders = new List<InitialOrderOutputModel>();

        //    ReviewService reviewService = new ReviewService();

        //    foreach (var orderDTO in orderDTOs)
        //    {
        //        InitialOrderOutputModel order = MapOrderDTOToOutputModel(orderDTO);

        //        orders.Add(order);
        //    }

        //    return orders;
        //}

        public ConfirmedOrderOutputModel GetOrderForContractorConfirmation(int orderId)
        {
            var orderDTO = OrderRepository.GetOrderForContractorConfirmation(orderId);

            var order = _mapper.Map<ConfirmedOrderOutputModel>(orderDTO);

            return order;
        }

        public void HideOrderById(int orderId)
        {
            OrderRepository.HideOrderById(orderId);
        }

        private InitialOrderOutputModel MapOrderDTOToOutputModel(OrderDTO orderDTO)
        {
            ReviewService reviewService = new ReviewService();

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
