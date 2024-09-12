using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;
using RepairServicesAggregatorBot.Bot.States.AdminStates;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ManageExistOrderStates
{
    public class CancelOrderState : AbstractState
    {
        private int _orderId;

        private OrderService _orderService;

        private UserService _userService;

        private int _messageId;

        public CancelOrderState(int orderId, int messageId)
        {
            _orderId = orderId;

            _orderService = new();

            _userService = new();

            _messageId = messageId;
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var order = _orderService.GetOrderSystemInfoById(_orderId);

            var updatedOrder = new ExtendedOrderInputModel()
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ContractorId = order.ContractorId,
                AdminId = order.AdminId,
                StatusId = 6,
                ServiceTypeId = order.ServiceTypeId,
                Date = order.Date,
                OrderDescription = order.OrderDescription,
                Address = order.Address,
                IsDeleted = order.IsDeleted
            };

            _orderService.UpdateOrder(updatedOrder);

            if (updatedOrder.StatusId >= 1 && updatedOrder.StatusId <= 5)
            {   
                var admin = _userService.GetUserById((int)order.AdminId);

                long adminChatId = admin.ChatId;

                await botClient.SendTextMessageAsync(adminChatId, $"Заказ {updatedOrder.Id} отменен.");
            }

            if (updatedOrder.StatusId >= 3 && updatedOrder.StatusId <= 5)
            {
                var contractor = _userService.GetUserById((int)order.ContractorId);

                long contractorChatId = contractor.ChatId;

                await botClient.SendTextMessageAsync(contractorChatId, $"Заказ {updatedOrder.Id} отменен.");
            }

            await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Ваш заказ отменен.");

            if (context.RoleId == 1)
            {
                context.State = new ClientMenuState();
            }
            else if (context.RoleId == 3)
            {
                context.State = new AdminMenuState();
            }

            context.State.ReactInBot(context, botClient);
        }
    }
}
