using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ManageExistOrderStates
{
    public class CompleteOrderState : AbstractState
    {
        private int _orderId;

        private OrderService _orderService;

        private int _messageId;

        public CompleteOrderState(int orderId, int messageId)
        {
            _orderId = orderId;

            _orderService = new();

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
                StatusId = 5,
                ServiceTypeId = order.ServiceTypeId,
                Date = order.Date,
                OrderDescription = order.OrderDescription,
                Address = order.Address,
                IsDeleted = order.IsDeleted

            };

            _orderService.UpdateOrder(updatedOrder);

            if (updatedOrder.StatusId > 1 && updatedOrder.StatusId < 5)
            {
                await botClient.SendTextMessageAsync(updatedOrder.AdminId, $"заказ {updatedOrder.Id} завершен!!");
            }

            if (updatedOrder.StatusId > 1 && updatedOrder.StatusId < 5)
            {
                await botClient.SendTextMessageAsync(updatedOrder.ContractorId, $"заказ {updatedOrder.Id} завершен!!");
            }

            await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId,  $"Ваш заказ завершен!!");

            context.State = new ClientMenuState();

            context.State.ReactInBot(context, botClient);
        }
    }
}
